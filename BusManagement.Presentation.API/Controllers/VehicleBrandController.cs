using BusManagement.Core.DataModel.DTOs;
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

    [HttpPost]
    public IActionResult Add([FromBody] BrandDTO brand)
    {
        var newBrand = _brandService.Add(brand);
        return CreatedAtAction(nameof(GetById), new { id = newBrand.Id }, newBrand);
    }
}
