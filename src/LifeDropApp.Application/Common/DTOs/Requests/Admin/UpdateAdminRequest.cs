using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeDropApp.Application.Common.DTOs.Requests.Admin;

public partial class UpdateAdminRequest
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = null!;
}
