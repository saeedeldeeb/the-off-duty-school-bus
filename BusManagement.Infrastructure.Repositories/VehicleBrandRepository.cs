using BusManagement.Core.Data;
using BusManagement.Core.Repositories;
using BusManagement.Infrastructure.Context;
using BusManagement.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BusManagement.Infrastructure.Repositories;

public class VehicleBrandRepository : BaseRepository<VehicleBrand>, IVehicleBrandRepository
{
    private readonly AppDbContext _context;

    public VehicleBrandRepository(AppDbContext context)
        : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<VehicleBrand>> GetAllAsync()
    {
        return await _context.VehicleBrands.Include(i => i.Translations).ToListAsync();
    }
}
