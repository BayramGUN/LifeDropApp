using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeDropApp.Application.Common.DTOs.Responses.Donor;

public class DonorResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string BloodType { get; set; } = null!;

    public string? Address { get; set; }

    public string Fullname { get; set; } = null!;

    public int? Point { get; set; }

    public int Age { get; set; }

}
