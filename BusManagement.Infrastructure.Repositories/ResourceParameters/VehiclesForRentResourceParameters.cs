using BusManagement.Core.Repositories.ResourceParameters;

namespace BusManagement.Infrastructure.Repositories.ResourceParameters;

public class VehiclesForRentResourceParameters : IVehiclesForRentResourceParameters
{
    private const int MaxPageSize = 20;

    public string? Brand { get; set; }
    public int? MinSeats { get; set; }
    public int? MaxSeats { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? SearchQuery { get; set; }

    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
