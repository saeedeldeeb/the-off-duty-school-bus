using Microsoft.AspNetCore.Authorization;

namespace BusManagement.Presentation.API.Filters;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; private set; } = permission;
}
