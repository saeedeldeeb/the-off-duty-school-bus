namespace BusManagement.Core.DataModel.DTOs;

public class SchoolDTO
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string? Email { get; set; }
    public Guid EmployeeId { get; set; }
}
