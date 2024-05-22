namespace BusManagement.Core.DataModel.ViewModels;

public class TripVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string StartPoint { get; set; }
    public string EndPoint { get; set; }
    public Guid? VehicleId { get; set; }
    public VehicleVM? Vehicle { get; set; }
}
