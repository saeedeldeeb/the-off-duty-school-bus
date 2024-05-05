using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Data;
using BusManagement.Core.Repositories;
using BusManagement.Core.Repositories.ResourceParameters;
using BusManagement.Infrastructure.Context;
using BusManagement.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BusManagement.Infrastructure.Repositories;

public class OffDutyRepository : BaseRepository<OffDuty>, IOffDutyRepository
{
    private readonly AppDbContext _context;

    public OffDutyRepository(AppDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<OffDuty>> GetAllWithParametersAsync(
        IOffDutyResourceParameters parameters
    )
    {
        ArgumentNullException.ThrowIfNull(parameters);

        var offDuties = _context.OffDuties.AsQueryable();
        if (parameters.VehicleId != null)
        {
            offDuties = offDuties.Where(v => v.VehicleId.ToString().Equals(parameters.VehicleId));
        }
        if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
        {
            offDuties = offDuties.Where(v =>
                v.FromDate.Equals(parameters.SearchQuery)
                || v.ToDate.Equals(parameters.SearchQuery)
                || v.FromTime.Equals(parameters.SearchQuery)
                || v.ToTime.Equals(parameters.SearchQuery)
            );
        }

        var count = await offDuties.CountAsync();
        var items = await offDuties
            .OrderBy(v => v.FromDate)
            .Include(v => v.Vehicle)
            .Skip(parameters.PageSize * (parameters.PageNumber - 1))
            .Take(parameters.PageSize)
            .ToListAsync();
        return PagedList<OffDuty>.CreateAsync(
            items,
            count,
            parameters.PageNumber,
            parameters.PageSize
        );
    }
}
