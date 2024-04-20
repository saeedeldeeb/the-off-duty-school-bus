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

    public override VehicleBrand Update(VehicleBrand entity, Guid id)
    {
        var brand = Find(x => x.Id == id, ["Translations"]);
        if (brand == null)
            throw new Exception("Brand not found");

        brand.Translations.Clear();
        brand.Translations = entity.Translations;
        brand.ModificationDateTime = DateTime.Now;

        _context.VehicleBrands.Update(brand);
        return brand;
    }
}
