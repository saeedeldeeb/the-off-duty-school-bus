using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Data;
using BusManagement.Core.Repositories;
using BusManagement.Core.Repositories.ResourceParameters;
using BusManagement.Infrastructure.Context;
using BusManagement.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BusManagement.Infrastructure.Repositories;

public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
{
    private readonly AppDbContext _context;

    public VehicleRepository(AppDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Vehicle>> GetAllWithParametersAsync(
        IVehicleResourceParameters parameters
    )
    {
        ArgumentNullException.ThrowIfNull(parameters);

        var vehicles = _context.Vehicles.AsQueryable();
        if (!string.IsNullOrWhiteSpace(parameters.Brand))
        {
            vehicles = vehicles.Where(v => v.BrandId.ToString().Equals(parameters.Brand));
        }
        if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
        {
            vehicles = vehicles.Where(v => v.PlateNumber.Contains(parameters.SearchQuery));
        }

        var count = await vehicles.CountAsync();
        var items = await vehicles
            .OrderBy(v => v.Capacity)
            .Include(v => v.Brand)
            .ThenInclude(b => b!.Translations)
            .Skip(parameters.PageSize * (parameters.PageNumber - 1))
            .Take(parameters.PageSize)
            .ToListAsync();
        return PagedList<Vehicle>.CreateAsync(
            items,
            count,
            parameters.PageNumber,
            parameters.PageSize
        );
    }

    public async Task<PagedList<Vehicle>> GetVehiclesForRentAsync(
        IVehiclesForRentResourceParameters parameters
    )
    {
        ArgumentNullException.ThrowIfNull(parameters);

        var vehicles = _context.Vehicles.AsQueryable();
        if (!string.IsNullOrWhiteSpace(parameters.Brand))
        {
            vehicles = vehicles.Where(v => v.BrandId.ToString().Equals(parameters.Brand));
        }
        if (parameters.MinSeats.HasValue)
        {
            vehicles = vehicles.Where(v => v.Capacity >= parameters.MinSeats);
        }
        if (parameters.MaxSeats.HasValue)
        {
            vehicles = vehicles.Where(v => v.Capacity <= parameters.MaxSeats);
        }
        if (parameters.From.HasValue)
        {
            vehicles = vehicles
                .Where(v =>
                    v.OffDuties.Any(r => r.FromDate >= DateOnly.FromDateTime(parameters.From.Value))
                )
                .Where(v =>
                    v.OffDuties.Any(r => r.FromTime >= TimeOnly.FromDateTime(parameters.From.Value))
                );
        }
        if (parameters.To.HasValue)
        {
            vehicles = vehicles
                .Where(v =>
                    v.OffDuties != null
                    && v.OffDuties.Any(r => r.ToDate >= DateOnly.FromDateTime(parameters.To.Value))
                )
                .Where(v =>
                    v.OffDuties != null
                    && v.OffDuties.Any(r => r.ToTime >= TimeOnly.FromDateTime(parameters.To.Value))
                );
        }
        if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
        {
            vehicles = vehicles.Where(v => v.PlateNumber.Contains(parameters.SearchQuery));
        }

        var count = await vehicles.CountAsync();
        var items = await vehicles
            .OrderBy(v => v.Capacity)
            .Include(v => v.OffDuties)
            .Include(v => v.Brand)
            .ThenInclude(b => b!.Translations)
            .Skip(parameters.PageSize * (parameters.PageNumber - 1))
            .Take(parameters.PageSize)
            .ToListAsync();
        return PagedList<Vehicle>.CreateAsync(
            items,
            count,
            parameters.PageNumber,
            parameters.PageSize
        );
    }
}
