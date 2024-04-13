using BusManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusManagement.Presentation.API.Controllers;

[Route("api/vehicle-brands")]
[ApiController]
public class VehicleBrandController : ControllerBase
{
    private readonly IVehicleBrandService _brandService;

    public VehicleBrandController(IVehicleBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var brands = await _brandService.GetAll();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var brand = await _brandService.GetById(id);
        return Ok(brand);
    }
}
