using System.ComponentModel.DataAnnotations;

namespace BusManagement.Core.DataModel.DTOs;

public class OffDutyDTO
{
    [Required]
    public Guid VehicleId { get; set; }

    [Required]
    public DateOnly FromDate { get; set; }

    [Required]
    public DateOnly ToDate { get; set; }

    [Required]
    public TimeOnly FromTime { get; set; }

    [Required]
    public TimeOnly ToTime { get; set; }
}
