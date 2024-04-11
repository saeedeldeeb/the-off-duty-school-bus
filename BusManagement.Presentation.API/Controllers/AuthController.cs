using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BusManagement.Presentation.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IStringLocalizer<AuthController> _localization;
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService, IStringLocalizer<AuthController> localization)
    {
        _authService = authService;
        _localization = localization;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(model);

        if (!result.IsAuthenticated)
            return BadRequest(_localization[result.Message].Value);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(model);

        if (!result.IsAuthenticated)
            return BadRequest(_localization[result.Message].Value);

        return Ok(result);
    }
}
