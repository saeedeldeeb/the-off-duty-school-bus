namespace BusManagement.Core.Repositories.ResourceParameters;

public interface IVehicleResourceParameters
{
    string? Brand { get; set; }
    string? SearchQuery { get; set; }
    int PageNumber { get; set; }
    int PageSize { get; set; }
}
