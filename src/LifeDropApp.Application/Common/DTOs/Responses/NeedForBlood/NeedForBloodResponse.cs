using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LifeDropApp.Application.Common.DTOs.Responses.Hospital;

namespace LifeDropApp.Application.Common.DTOs.Responses.NeedForBlood;

public class NeedForBloodResponse
{
    public Guid Id { get; set; }
    public string BloodType { get; set; } = null!;
    public int QuantityNeeded { get; set; }
    public HospitalResponse Hospital { get; set; } = null!;
}
