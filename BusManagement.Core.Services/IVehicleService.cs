using BusManagement.Core.Common.Helpers;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;
using BusManagement.Core.Repositories.ResourceParameters;

namespace BusManagement.Core.Services;

public interface IVehicleService
{
    Task<PagedList<VehicleVM>> GetAll(IVehicleResourceParameters parameters);
    Task<PagedList<VehiclesForRentVM>> GetVehiclesForRent(
        IVehiclesForRentResourceParameters parameters
    );
    Task<VehicleVM> GetById(Guid id);
    VehicleVM Add(VehicleDTO vehicle);
    VehicleVM Update(VehicleDTO vehicle, Guid id);
    VehicleVM PartialUpdate(VehicleDTO vehicle, Guid id);
    void Delete(Guid id);
}
