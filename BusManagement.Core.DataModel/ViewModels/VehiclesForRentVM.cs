namespace BusManagement.Core.DataModel.ViewModels;

public class VehiclesForRentVM
{
    public Guid Id { get; set; }
    public string PlateNumber { get; set; }
    public BrandVM Brand { get; set; }
    public int Capacity { get; set; }
    public DateOnly Year { get; set; }
    public ICollection<OffDutyVM> OffDuties { get; set; }
}
