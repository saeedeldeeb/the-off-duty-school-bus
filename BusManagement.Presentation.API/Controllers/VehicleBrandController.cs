using BusManagement.Core.Common.Constants;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusManagement.Presentation.API.Controllers;

[Route("api/vehicle-brands")]
[ApiController]
[Authorize(Roles = "SchoolTransportationManager")]
public class VehicleBrandController : ControllerBase
{
    private readonly IVehicleBrandService _brandService;

    public VehicleBrandController(IVehicleBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    [Authorize(Permissions.VehicleBrand.View)]
    public async Task<IActionResult> GetAll()
    {
        var brands = await _brandService.GetAll();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    [Authorize(Permissions.VehicleBrand.View)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var brand = await _brandService.GetById(id);
        return Ok(brand);
    }

    [HttpPost]
    [Authorize(Permissions.VehicleBrand.Create)]
    public IActionResult Add([FromBody] BrandDTO brand)
    {
        var newBrand = _brandService.Add(brand);
        return CreatedAtAction(nameof(GetById), new { id = newBrand.Id }, newBrand);
    }

    [HttpPut("{id}")]
    [Authorize(Permissions.VehicleBrand.Edit)]
    public IActionResult Update([FromBody] BrandDTO brand, Guid id)
    {
        var updatedBrand = _brandService.Update(brand, id);
        return Ok(updatedBrand);
    }
}
