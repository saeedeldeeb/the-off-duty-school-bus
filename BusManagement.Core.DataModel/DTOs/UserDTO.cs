using BusManagement.Core.Common.Enums;

namespace BusManagement.Core.DataModel.DTOs;

public class UserDTO
{
    public string Name { get; set; }
    public GenderEnum GenderEnum { get; set; }
    public string? ProfilePicture { get; set; }
}
