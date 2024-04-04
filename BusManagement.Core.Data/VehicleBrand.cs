using BusManagement.Core.Data.Base;

namespace BusManagement.Core.Data;

public class VehicleBrand : IEntity<Guid>, ICreatingTimeStamp, IUpdatingTimeStamp
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime? ModificationDateTime { get; set; }
}
