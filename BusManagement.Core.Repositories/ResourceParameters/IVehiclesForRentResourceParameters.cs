namespace BusManagement.Core.Repositories.ResourceParameters;

public interface IVehiclesForRentResourceParameters
{
    string? Brand { get; set; }
    int? MinSeats { get; set; }
    int? MaxSeats { get; set; }
    DateTime? From { get; set; }
    DateTime? To { get; set; }
    string? SearchQuery { get; set; }
    int PageNumber { get; set; }
    int PageSize { get; set; }
}
