using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LifeDropApp.Application.Common.DTOs.Requests.Address;

namespace LifeDropApp.Application.Common.DTOs.Requests.Donor;

public class CreateDonorRequest
{

    public Guid UserId { get; set; }

    [Required]
    [MaxLength(3)]
    public string BloodType { get; set; } = null!;
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public int? Point { get; set; }
    [Required]
    [Range(18, 150)]
    public int Age { get; set; }
    public CreateAddressRequest? Address { get; set; } 

}
