using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LifeDropApp.Application.Common.DTOs.Requests.Address;

namespace LifeDropApp.Application.Common.DTOs.Requests.Hospital;

public class CreateHospitalRequest
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = null!;
    [Required]
    public CreateAddressRequest Address { get; set; } = null!;

}
