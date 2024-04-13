using BusManagement.Core.DataModel.ViewModels;

namespace BusManagement.Core.Services;

public interface IVehicleBrandService
{
    Task<IEnumerable<BrandVM>> GetAll();
    Task<BrandVM> GetById(Guid id);
    BrandVM Add(BrandVM brand);
    BrandVM Update(BrandVM brand);
    void Delete(Guid id);
}
