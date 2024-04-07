using System.ComponentModel.DataAnnotations;

namespace BusManagement.Core.DataModel.DTOs;

public class LoginDTO
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
