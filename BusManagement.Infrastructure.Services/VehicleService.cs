using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Data;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;
using BusManagement.Core.Repositories;
using BusManagement.Core.Repositories.Base;
using BusManagement.Core.Repositories.ResourceParameters;
using BusManagement.Core.Services;
using BusManagement.Infrastructure.DataStructureMapping;

namespace BusManagement.Infrastructure.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VehicleService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
    {
        _vehicleRepository = vehicleRepository;
        _unitOfWork = unitOfWork;
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

    public async Task<VehicleVM> GetById(Guid id)
    {
        var vehicle = await _vehicleRepository.FindAsync(x => x.Id == id, ["Brand.Translations"]);
        if (vehicle == null)
        {
            throw new Exception("Vehicle not found");
        }
        return vehicle.Parse<Vehicle, VehicleVM>();
    }

    public VehicleVM Add(VehicleDTO vehicle)
    {
        var vehicleEntity = vehicle.Parse<VehicleDTO, Vehicle>();
        var addedVehicle = _vehicleRepository.Add(vehicleEntity);
        _unitOfWork.Complete();

        return addedVehicle.Parse<Vehicle, VehicleVM>();
    }

    public VehicleVM Update(VehicleDTO vehicle, Guid id)
    {
        var vehicleEntity = vehicle.Parse<VehicleDTO, Vehicle>();
        vehicleEntity.Id = id;
        var updatedVehicle = _vehicleRepository.Update(vehicleEntity, id);
        _unitOfWork.Complete();

        return updatedVehicle.Parse<Vehicle, VehicleVM>();
    }

    public VehicleVM PartialUpdate(VehicleDTO vehicle, Guid id)
    {
        var vehicleEntity = vehicle.Parse<VehicleDTO, Vehicle>();
        vehicleEntity.Id = id;
        var updatedVehicle = _vehicleRepository.Update(vehicleEntity, id);
        _unitOfWork.Complete();

        return updatedVehicle.Parse<Vehicle, VehicleVM>();
    }

    public void Delete(Guid id)
    {
        var vehicle = _vehicleRepository.GetById(id);
        if (vehicle == null)
        {
            throw new Exception("Vehicle not found");
        }

        _vehicleRepository.Delete(vehicle);
        _unitOfWork.Complete();
    }
}
