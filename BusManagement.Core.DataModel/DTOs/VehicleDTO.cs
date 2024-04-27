using System.ComponentModel.DataAnnotations;

namespace BusManagement.Core.DataModel.DTOs;

public class VehicleDTO
{
    [Required]
    public string PlateNumber { get; set; }

    [Required]
    public Guid BrandId { get; set; }

    [Required]
    public int Capacity { get; set; }

    // TODO: Swagger does not convert DateOnly type as string in the request body
    [Required]
    public DateOnly Year { get; set; }
}
