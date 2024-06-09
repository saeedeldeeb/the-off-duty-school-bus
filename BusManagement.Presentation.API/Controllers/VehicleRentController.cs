using System.Security.Claims;
using BusManagement.Core.Common.Constants;
using BusManagement.Core.Common.Enums;
using BusManagement.Core.DataModel.DTOs;
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
    private readonly IRentService _rentService;

    public VehicleRentController(IVehicleService vehicleService, IRentService rentService)
    {
        _vehicleService = vehicleService;
        _rentService = rentService;
    }

    [HttpGet]
    [Authorize(Permissions.Rent.View)]
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

    [HttpPost]
    [Authorize(Permissions.Rent.Create)]
    public async Task<ActionResult> RentVehicle([FromBody] RentDTO rent)
    {
        rent.UserId = Guid.Parse(User.FindFirstValue("uid") ?? string.Empty);
        rent.Status = RentStatusEnum.Pending;
        var rentedVehicle = await _rentService.CreateRentAsync(rent);
        return StatusCode(StatusCodes.Status201Created, rentedVehicle);
    }

    [HttpPut("{id}")]
    [Authorize(Permissions.Rent.Edit)]
    public async Task<ActionResult> UpdateRent(Guid id, [FromBody] RentDTO rent)
    {
        rent.UserId = Guid.Parse(User.FindFirstValue("uid") ?? string.Empty);
        if (rent.Cancel)
        {
            rent.Status = RentStatusEnum.Canceled;
        }
        var updatedRent = await _rentService.UpdateRentAsync(rent, id);
        return Ok(updatedRent);
    }
}
