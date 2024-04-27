using BusManagement.Core.Common.Constants;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.Services;
using BusManagement.Infrastructure.Repositories.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusManagement.Presentation.API.Controllers;

[ApiController]
[Route("api/vehicles")]
[Authorize(Roles = "SchoolTransportationManager")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    [Authorize(Permissions.Vehicle.View)]
    public async Task<IActionResult> GetAll([FromQuery] VehicleResourceParameters parameters)
    {
        var vehicles = await _vehicleService.GetAll(parameters);
        return Ok(
            new
            {
                vehicles.CurrentPage,
                vehicles.PageSize,
                vehicles.TotalCount,
                vehicles.TotalPages,
                vehicles
            }
        );
    }

    [HttpGet("{id}")]
    [Authorize(Permissions.Vehicle.View)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var vehicle = await _vehicleService.GetById(id);
        return Ok(vehicle);
    }

    [HttpPost]
    [Authorize(Permissions.Vehicle.Create)]
    public IActionResult Add([FromBody] VehicleDTO vehicle)
    {
        var addedVehicle = _vehicleService.Add(vehicle);
        return CreatedAtAction(nameof(GetById), new { id = addedVehicle.Id }, addedVehicle);
    }

    [HttpPut("{id}")]
    [Authorize(Permissions.Vehicle.Edit)]
    public IActionResult Update([FromBody] VehicleDTO vehicle, Guid id)
    {
        var updatedVehicle = _vehicleService.Update(vehicle, id);
        return Ok(updatedVehicle);
    }

    [HttpDelete("{id}")]
    [Authorize(Permissions.Vehicle.Delete)]
    public IActionResult Delete(Guid id)
    {
        _vehicleService.Delete(id);
        return NoContent();
    }
}
