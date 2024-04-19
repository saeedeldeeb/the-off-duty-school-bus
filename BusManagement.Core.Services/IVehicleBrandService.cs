using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;

namespace BusManagement.Core.Services;

public interface IVehicleBrandService
{
    Task<IEnumerable<BrandVM>> GetAll();
    Task<BrandVM> GetById(Guid id);
    BrandVM Add(BrandDTO brand);
    BrandVM Update(BrandDTO brand);
    void Delete(Guid id);
}
