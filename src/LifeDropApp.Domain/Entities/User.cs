using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeDropApp.Domain.Entities;

public partial class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid(); 

    public string Username { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? Role { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    public virtual ICollection<Donor> Donors { get; set; } = new List<Donor>();

    public virtual ICollection<Hospital> Hospitals { get; set; } = new List<Hospital>();
}
