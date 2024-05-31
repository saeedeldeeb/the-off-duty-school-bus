using BusManagement.Core.Common.Enums;

namespace BusManagement.Core.DataModel.ViewModels;

public class UserVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public GenderEnum Gender { get; set; }
    public string? ProfilePicture { get; set; }
}
