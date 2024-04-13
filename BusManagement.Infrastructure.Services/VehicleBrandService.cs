using AutoMapper;
using BusManagement.Core.Data;
using BusManagement.Core.DataModel.ViewModels;
using BusManagement.Core.Repositories;
using BusManagement.Core.Repositories.Base;
using BusManagement.Core.Services;
using BusManagement.Infrastructure.DataStructureMapping;

namespace BusManagement.Infrastructure.Services;

public class VehicleBrandService : IVehicleBrandService
{
    private readonly IVehicleBrandRepository _brandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VehicleBrandService(
        IVehicleBrandRepository brandRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _brandRepository = brandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BrandVM>> GetAll()
    {
        var brands = await _brandRepository.GetAllAsync();
        return brands.Parse<IEnumerable<VehicleBrand>, IEnumerable<BrandVM>>();
    }

    public async Task<BrandVM> GetById(Guid id)
    {
        var brand = await _brandRepository.FindAsync(x => x.Id == id, ["Translations"]);
        return brand.Parse<VehicleBrand, BrandVM>();
    }

    public BrandVM Add(BrandVM brand)
    {
        throw new NotImplementedException();
    }

    public BrandVM Update(BrandVM brand)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
