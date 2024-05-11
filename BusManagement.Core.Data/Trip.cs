using BusManagement.Core.Data.Base;
using NetTopologySuite.Geometries;

namespace BusManagement.Core.Data;

public class Trip : IEntity<Guid>, ICreatingTimeStamp, IUpdatingTimeStamp
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Point StartPoint { get; set; }
    public Point EndPoint { get; set; }
    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime? ModificationDateTime { get; set; }
}
