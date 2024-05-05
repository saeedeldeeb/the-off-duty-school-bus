namespace BusManagement.Core.DataModel.ViewModels;

public class VehicleVM
{
    public Guid Id { get; set; }
    public string PlateNumber { get; set; }
    public BrandVM Brand { get; set; }
    public int Capacity { get; set; }
    public DateOnly Year { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime? ModificationDateTime { get; set; }
}
