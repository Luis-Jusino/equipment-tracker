using System.ComponentModel.DataAnnotations;

namespace EquipmentTracker.Api.Models;

public enum EquipmentStatus
{
    Operational,
    NeedsMaintenance,
    OutOfService
}

public class Equipment
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name {get; set;} = string.Empty;

    [Required, StringLength(50)]
    public string SerialNumber {get; set;} = string.Empty;

    public EquipmentStatus Status {get; set;} = EquipmentStatus.Operational;

    public DateTime? LastInspected {get; set;}
}