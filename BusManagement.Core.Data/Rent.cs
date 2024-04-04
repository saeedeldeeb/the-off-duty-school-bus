using BusManagement.Core.Common.Enums;
using BusManagement.Core.Data.Base;

namespace BusManagement.Core.Data;

public class Rent : IEntity<Guid>, ICreatingTimeStamp, IUpdatingTimeStamp
{
    public Guid Id { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public Guid VehicleId { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public RentStatusEnum Status { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime? ModificationDateTime { get; set; }
}
