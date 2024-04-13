using System.ComponentModel.DataAnnotations;
using BusManagement.Core.Data.MultiLingualObjects;

namespace BusManagement.Core.Data;

public class VehicleBrandTranslation : IObjectTranslation
{
    [Required]
    public Guid VehicleBrandId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Language { get; set; }
}
