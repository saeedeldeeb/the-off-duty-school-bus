using BusManagement.Core.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace BusManagement.Infrastructure.Context;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public GenderEnum Gender { get; set; }
    public string? ProfilePicture { get; set; }
}
