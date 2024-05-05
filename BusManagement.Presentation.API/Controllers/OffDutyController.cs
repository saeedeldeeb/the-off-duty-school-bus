using BusManagement.Core.Common.Constants;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.Services;
using BusManagement.Infrastructure.Repositories.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusManagement.Presentation.API.Controllers;

[ApiController]
[Route("api/off-duties")]
[Authorize(Roles = "SchoolTransportationManager")]
public class OffDutyController : ControllerBase
{
    private readonly IOffDutiesService _offDutyService;

    public OffDutyController(IOffDutiesService offDutyService)
    {
        _offDutyService = offDutyService;
    }

    [HttpGet]
    [Authorize(Permissions.OffDuty.View)]
    public async Task<IActionResult> GetAll([FromQuery] OffDutyResourceParameters parameters)
    {
        var offDuties = await _offDutyService.GetAll(parameters);
        return Ok(
            new
            {
                offDuties.CurrentPage,
                offDuties.PageSize,
                offDuties.TotalCount,
                offDuties.TotalPages,
                offDuties
            }
        );
    }

    [HttpGet("{id:guid}")]
    [Authorize(Permissions.OffDuty.View)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var offDuty = await _offDutyService.GetById(id);
        return Ok(offDuty);
    }

    [HttpPost]
    [Authorize(Permissions.OffDuty.Create)]
    public IActionResult Add([FromBody] OffDutyDTO offDuty)
    {
        var addedOffDuty = _offDutyService.Add(offDuty);
        return CreatedAtAction(nameof(GetById), new { id = addedOffDuty.Id }, addedOffDuty);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Permissions.OffDuty.Edit)]
    public IActionResult Update([FromBody] OffDutyDTO offDuty, Guid id)
    {
        var updatedOffDuty = _offDutyService.Update(offDuty, id);
        return Ok(updatedOffDuty);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Permissions.OffDuty.Delete)]
    public IActionResult Delete(Guid id)
    {
        _offDutyService.Delete(id);
        return NoContent();
    }
}
