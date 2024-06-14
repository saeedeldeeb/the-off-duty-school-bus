using BusManagement.Core.Common.Constants;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.Services;
using BusManagement.Infrastructure.Repositories.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusManagement.Presentation.API.Controllers;

[ApiController]
[Route("api/trips")]
[Authorize(Roles = "CompanyTransportationManager")]
public class TripController : ControllerBase
{
    private readonly ITripsService _tripService;

    public TripController(ITripsService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    [Authorize(Permissions.Trip.View)]
    public async Task<IActionResult> GetAll([FromQuery] TripResourceParameters parameters)
    {
        var trips = await _tripService.GetAll(parameters);
        return Ok(
            new
            {
                trips.CurrentPage,
                trips.PageSize,
                trips.TotalCount,
                trips.TotalPages,
                trips
            }
        );
    }

    [HttpGet("{id:guid}")]
    [Authorize(Permissions.Trip.View)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var trip = await _tripService.GetById(id);
        return Ok(trip);
    }

    [HttpPost]
    [Authorize(Permissions.Trip.Create)]
    public IActionResult Add([FromBody] TripDTO trip)
    {
        var addedTrip = _tripService.Add(trip);
        return CreatedAtAction(nameof(GetById), new { id = addedTrip.Id }, addedTrip);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Permissions.Trip.Edit)]
    public IActionResult Update([FromBody] TripDTO trip, Guid id)
    {
        var updatedTrip = _tripService.Update(trip, id);
        return Ok(updatedTrip);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Permissions.Trip.Delete)]
    public IActionResult Delete(Guid id)
    {
        _tripService.Delete(id);
        return NoContent();
    }

    [HttpPost("{id:guid}/start")]
    public IActionResult StartTrip(Guid id)
    {
        _tripService.StartTrip(id);
        return NoContent();
    }
}
