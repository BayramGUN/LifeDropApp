using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeDropApp.Domain.Entities;

public partial class Address
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Street { get; set; }
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? ZipCode { get; set; }
    public virtual ICollection<Donor> Donors { get; set; } = new List<Donor>();
    public virtual ICollection<Hospital> Hospitals { get; set; } = new List<Hospital>();
}
