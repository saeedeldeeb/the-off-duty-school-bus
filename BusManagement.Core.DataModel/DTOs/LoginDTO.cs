using System.ComponentModel.DataAnnotations;

namespace BusManagement.Core.DataModel.DTOs;

public class LoginDTO
{
    [Required]
    public string Email { get; set; }

    [Required(ErrorMessage = "password_required")]
    public string Password { get; set; }
}
