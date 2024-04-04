using BusManagement.Core.Data.Base;
using NetTopologySuite.Geometries;

namespace BusManagement.Core.Data;

public class School : IEntity<Guid>, ICreatingTimeStamp, IUpdatingTimeStamp
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Point Location { get; set; }
    public string? Email { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime? ModificationDateTime { get; set; }
}
