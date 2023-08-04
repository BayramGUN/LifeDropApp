using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LifeDropApp.Application.Common.DTOs.Requests.Address;

namespace LifeDropApp.Application.Common.DTOs.Requests.Donor;

public class UpdateDonorRequest
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string BloodType { get; set; } = null!;

    public UpdateAddressRequest? Address { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public bool IsDonate { get; set; }

    public int Age { get; set; }

}
