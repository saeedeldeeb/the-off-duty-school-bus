using BusManagement.Core.Data;
using BusManagement.Core.DataModel.DTOs;
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

    public VehicleBrandService(IVehicleBrandRepository brandRepository, IUnitOfWork unitOfWork)
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

    public BrandVM Add(BrandDTO brandDto)
    {
        var brandEntity = brandDto.Parse<BrandDTO, VehicleBrand>();
        var brand = _brandRepository.Add(brandEntity);
        _unitOfWork.Complete();
        return brand.Parse<VehicleBrand, BrandVM>();
    }

    public BrandVM Update(BrandDTO brand, Guid id)
    {
        var brandEntity = brand.Parse<BrandDTO, VehicleBrand>();
        var updatedBrand = _brandRepository.Update(brandEntity, id);
        _unitOfWork.Complete();
        return updatedBrand.Parse<VehicleBrand, BrandVM>();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
