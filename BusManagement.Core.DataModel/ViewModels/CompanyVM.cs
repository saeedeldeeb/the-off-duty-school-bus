namespace BusManagement.Core.DataModel.ViewModels;

public class CompanyVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string? Email { get; set; }
    public UserVM Employee { get; set; }
}
