using BusManagement.Core.Common.Enums;

namespace BusManagement.Core.DataModel.ViewModels;

public class RentVM
{
    public Guid Id { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public Guid VehicleId { get; set; }
    public string UserId { get; set; }
    public RentStatusEnum Status { get; set; }
}
