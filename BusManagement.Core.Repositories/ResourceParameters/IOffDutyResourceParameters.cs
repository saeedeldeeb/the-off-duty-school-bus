namespace BusManagement.Core.Repositories.ResourceParameters;

public interface IOffDutyResourceParameters
{
    Guid? VehicleId { get; set; }
    string? SearchQuery { get; set; }
    int PageNumber { get; set; }
    int PageSize { get; set; }
}
