using BusManagement.Core.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BusManagement.Presentation.API.Filters;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public PermissionAuthorizationHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement
    )
    {
        var user = await _userManager.GetUserAsync(context.User);
        if (user == null)
        {
            context.Fail();
            return;
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        foreach (var roleName in userRoles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                continue;
            var roleClaims = await _roleManager.GetClaimsAsync(role);
            if (!roleClaims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission))
                continue;
            context.Succeed(requirement);
            return;
        }

        context.Fail();
    }
}
