using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BusManagement.Core.Common.Enums;

namespace BusManagement.Core.DataModel.DTOs;

public class RentDTO
{
    [Required]
    public DateTime From { get; set; }

    [Required]
    public DateTime To { get; set; }

    [Required]
    public Guid VehicleId { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }

    [JsonIgnore]
    public RentStatusEnum Status { get; set; }
    public bool Cancel { get; set; } = false;
}
