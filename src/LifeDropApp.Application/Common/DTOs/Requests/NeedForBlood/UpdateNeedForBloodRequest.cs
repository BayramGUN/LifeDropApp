using System.ComponentModel.DataAnnotations;

namespace LifeDropApp.Application.Common.DTOs.Requests.NeedForBlood;

public class UpdateNeedForBloodRequest
{
    public Guid Id { get; set; }
    [Required]
    public Guid HospitalId { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(3)]
    public string BloodType { get; set; } = null!;
    [Required]
    [Range(1, int.MaxValue)]
    public int QuantityNeeded { get; set; }

}
