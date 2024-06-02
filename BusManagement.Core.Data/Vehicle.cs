using System.ComponentModel.DataAnnotations.Schema;
using BusManagement.Core.Data.Base;

namespace BusManagement.Core.Data;

public class Vehicle : IEntity<Guid>, ICreatingTimeStamp, IUpdatingTimeStamp
{
    public Guid Id { get; set; }
    public string PlateNumber { get; set; }
    public Guid BrandId { get; set; }

    [ForeignKey(nameof(BrandId))]
    public VehicleBrand Brand { get; set; }
    public ICollection<OffDuty>? OffDuties { get; set; }
    public int Capacity { get; set; }
    public DateOnly Year { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime? ModificationDateTime { get; set; }
}
