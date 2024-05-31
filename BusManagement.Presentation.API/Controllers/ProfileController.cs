using System.Security.Claims;
using BusManagement.Core.Common.Constants;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusManagement.Presentation.API.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    [Authorize(
        Roles = "CompanyTransportationManager,SchoolTransportationManager",
        Policy = Permissions.Profile.View
    )]
    public async Task<IActionResult> GetProfile()
    {
        var profile = await _profileService.GetUserProfile(
            Guid.Parse(User.FindFirstValue("uid") ?? string.Empty)
        );
        return Ok(profile);
    }

    [HttpPut]
    [Authorize(
        Roles = "CompanyTransportationManager,SchoolTransportationManager",
        Policy = Permissions.Profile.Edit
    )]
    public async Task<IActionResult> UpdateProfile([FromBody] UserDTO profile)
    {
        var updatedProfile = await _profileService.UpdateUserProfile(
            profile,
            Guid.Parse(User.FindFirstValue("uid") ?? string.Empty)
        );
        return Ok(updatedProfile);
    }

    [HttpPut("profile-picture")]
    [Authorize(
        Roles = "CompanyTransportationManager,SchoolTransportationManager",
        Policy = Permissions.Profile.Edit
    )]
    public async Task<IActionResult> UpdateProfilePicture(IFormFile profilePicture)
    {
        if (profilePicture.Length == 0)
        {
            return BadRequest("Profile picture is required");
        }

        string filePath;
        try
        {
            filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                $"{Path.GetFileNameWithoutExtension(profilePicture.FileName)}_{DateTime.Now:yyyy-MM-dd HH-mm-ss}{Path.GetExtension(profilePicture.FileName)}"
            );
            await using var stream = new FileStream(filePath, FileMode.Create);
            await profilePicture.CopyToAsync(stream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        await _profileService.UpdateProfilePicture(
            filePath.Replace(Directory.GetCurrentDirectory(), string.Empty),
            Guid.Parse(User.FindFirstValue("uid") ?? string.Empty)
        );
        return NoContent();
    }

    [HttpGet("school")]
    [Authorize(Roles = "SchoolTransportationManager", Policy = Permissions.Profile.View)]
    public async Task<IActionResult> GetSchoolProfile()
    {
        var profile = await _profileService.GetSchoolProfile(
            Guid.Parse(User.FindFirstValue("uid") ?? string.Empty)
        );
        return Ok(profile);
    }

    [HttpGet("company")]
    [Authorize(Roles = "CompanyTransportationManager", Policy = Permissions.Profile.View)]
    public async Task<IActionResult> GetCompanyProfile()
    {
        var profile = await _profileService.GetCompanyProfile(
            Guid.Parse(User.FindFirstValue("uid") ?? string.Empty)
        );
        return Ok(profile);
    }

    [HttpPut("school")]
    [Authorize(Roles = "SchoolTransportationManager", Policy = Permissions.Profile.Edit)]
    public async Task<IActionResult> UpdateSchoolProfile([FromBody] SchoolDTO school)
    {
        var userGuid = Guid.Parse(User.FindFirstValue("uid") ?? string.Empty);
        school.EmployeeId = userGuid;
        var updatedProfile = await _profileService.UpdateSchoolProfile(school, userGuid);
        return Ok(updatedProfile);
    }

    [HttpPut("company")]
    [Authorize(Roles = "CompanyTransportationManager", Policy = Permissions.Profile.Edit)]
    public async Task<IActionResult> UpdateCompanyProfile([FromBody] CompanyDTO company)
    {
        var updatedProfile = await _profileService.UpdateCompanyProfile(
            company,
            Guid.Parse(User.FindFirstValue("uid") ?? string.Empty)
        );
        return Ok(updatedProfile);
    }
}
