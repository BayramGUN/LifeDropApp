using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeDropApp.Domain.Entities;

public partial class Donor
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public string BloodType { get; set; } = null!;

    public Guid? AddressId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int Point { get; set; }

    public int Age { get; set; }

    public virtual Address? Address { get; set; }

    public virtual User User { get; set; } = null!;
}
