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

    public override Vehicle Update(Vehicle entity, Guid id)
    {
        entity.Id = id;
        entity.ModificationDateTime = DateTime.Now;

        _context.Vehicles.Update(entity);
        return entity;
    }
}
