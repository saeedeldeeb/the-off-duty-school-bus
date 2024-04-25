using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Data;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;
using BusManagement.Core.Repositories;
using BusManagement.Core.Repositories.ResourceParameters;
using BusManagement.Core.Services;
using BusManagement.Infrastructure.DataStructureMapping;

namespace BusManagement.Infrastructure.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<PagedList<VehicleVM>> GetAll(IVehicleResourceParameters parameters)
    {
        var pagedVehicles = await _vehicleRepository.GetAllWithParametersAsync(parameters);
        var vehicles = pagedVehicles.Parse<IEnumerable<Vehicle>, IEnumerable<VehicleVM>>();

        return PagedList<VehicleVM>.CreateAsync(
            vehicles,
            pagedVehicles.TotalCount,
            pagedVehicles.CurrentPage,
            pagedVehicles.PageSize
        );
    }

    public Task<VehicleVM> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public VehicleVM Add(VehicleDTO vehicle)
    {
        throw new NotImplementedException();
    }

    public VehicleVM Update(VehicleDTO vehicle, Guid id)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
