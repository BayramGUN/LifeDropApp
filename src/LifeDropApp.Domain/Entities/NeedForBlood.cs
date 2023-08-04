using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeDropApp.Domain.Entities;

public partial class NeedForBlood
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid HospitalId { get; set; }

    public string BloodType { get; set; } = null!;

    public int QuantityNeeded { get; set; }

    public virtual Hospital Hospital { get; set; } = null!;
}
