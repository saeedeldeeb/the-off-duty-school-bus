namespace BusManagement.Core.DataModel.ViewModels;

public class OffDutyVM
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public VehicleVM Vehicle { get; set; }
    public DateOnly FromDate { get; set; }
    public DateOnly ToDate { get; set; }
    public TimeOnly FromTime { get; set; }
    public TimeOnly ToTime { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}
