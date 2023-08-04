using System;
using System.Collections.Generic;

namespace LifeDropApp.Application.Common.DTOs.Requests.Admin;

public class CreateAdminRequest
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
}
