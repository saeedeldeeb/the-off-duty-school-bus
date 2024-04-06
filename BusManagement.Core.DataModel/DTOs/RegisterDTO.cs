using System.ComponentModel.DataAnnotations;
using BusManagement.Core.Common.Enums;

namespace BusManagement.Core.DataModel.DTOs;

public class RegisterDTO
{
    [Required, StringLength(50)]
    public string Name { get; set; }

    [Required]
    public GenderEnum Gender { get; set; }

    [Required, StringLength(128)]
    public string Email { get; set; }

    [Required, StringLength(256)]
    public string Password { get; set; }

    [Required, Compare("Password")]
    public string ConfirmPassword { get; set; }

    [
        Required,
        RegularExpression(
            @"^(?:(?:\+|00)20)?(10|11|12|13|14|15|16|17|18|19)[0-9]{8}$",
            ErrorMessage = "Invalid phone number"
        )
    ]
    public string PhoneNumber { get; set; }
}
