using BusManagement.Core.Common.Constants;
using BusManagement.Core.Services;
using BusManagement.Infrastructure.Repositories.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusManagement.Presentation.API.Controllers;

[ApiController]
[Route("api/vehicle-rent")]
[Authorize(Roles = "CompanyTransportationManager")]
public class VehicleRentController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehicleRentController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] VehiclesForRentResourceParameters parameters
    )
    {
        var vehicles = await _vehicleService.GetVehiclesForRent(parameters);
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
