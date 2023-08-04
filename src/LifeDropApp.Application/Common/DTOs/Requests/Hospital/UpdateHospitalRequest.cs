
using System.ComponentModel.DataAnnotations;
using LifeDropApp.Application.Common.DTOs.Requests.Address;

namespace LifeDropApp.Application.Common.DTOs.Requests.Hospital;

public class UpdateHospitalRequest
{
    [Required]
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string? Name { get; set; } = null!;
    public UpdateAddressRequest? Address { get; set; }
}
