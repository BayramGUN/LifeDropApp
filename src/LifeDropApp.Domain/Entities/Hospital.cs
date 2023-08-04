using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeDropApp.Domain.Entities;

public partial class Hospital
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public Guid AddressId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<NeedForBlood> NeedForBloods { get; set; } = new List<NeedForBlood>();

    public virtual User User { get; set; } = null!;
}
