using System.ComponentModel.DataAnnotations;

namespace BusManagement.Core.DataModel.DTOs;

public class TripDTO
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string StartPoint { get; set; }

    [Required]
    public string EndPoint { get; set; }

    public Guid? VehicleId { get; set; }
}
