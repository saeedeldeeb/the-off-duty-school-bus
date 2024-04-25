using BusManagement.Core.Common.Constants;
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
}
