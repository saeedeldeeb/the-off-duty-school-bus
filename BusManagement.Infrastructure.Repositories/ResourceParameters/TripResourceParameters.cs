using BusManagement.Core.Repositories.ResourceParameters;

namespace BusManagement.Infrastructure.Repositories.ResourceParameters;

public class TripResourceParameters : ITripResourceParameters
{
    private const int MaxPageSize = 20;
    public Guid? VehicleId { get; set; }
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
