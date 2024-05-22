using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Data;
using BusManagement.Core.Repositories;
using BusManagement.Core.Repositories.ResourceParameters;
using BusManagement.Infrastructure.Context;
using BusManagement.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BusManagement.Infrastructure.Repositories;

public class TripRepository : BaseRepository<Trip>, ITripRepository
{
    private readonly AppDbContext _context;

    public TripRepository(AppDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Trip>> GetAllWithParametersAsync(ITripResourceParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        var offDuties = _context.Trips.AsQueryable();
        if (parameters.VehicleId != null)
        {
            offDuties = offDuties.Where(v =>
                v.VehicleId != null && v.VehicleId.ToString().Equals(parameters.VehicleId)
            );
        }
        if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
        {
            offDuties = offDuties.Where(v => v.Name.Equals(parameters.SearchQuery));
        }

        var count = await offDuties.CountAsync();
        var items = await offDuties
            .OrderBy(v => v.Name)
            .Include(v => v.Vehicle)
            .Skip(parameters.PageSize * (parameters.PageNumber - 1))
            .Take(parameters.PageSize)
            .ToListAsync();
        return PagedList<Trip>.CreateAsync(
            items,
            count,
            parameters.PageNumber,
            parameters.PageSize
        );
    }
}
