using BusManagement.Core.Data.Base;
using BusManagement.Core.Data.MultiLingualObjects;

namespace BusManagement.Core.Data;

public class VehicleBrand
    : IEntity<Guid>,
        ICreatingTimeStamp,
        IUpdatingTimeStamp,
        IMultiLingualObject<VehicleBrandTranslation>
{
    public Guid Id { get; set; }
    public ICollection<VehicleBrandTranslation> Translations { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime? ModificationDateTime { get; set; }
}
