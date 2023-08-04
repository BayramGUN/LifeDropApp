using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeDropApp.Application.Common.DTOs.Requests.Address;

public class UpdateAddressRequest
{
    public Guid Id { get; set; }
    public string? Street { get; set; }

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? ZipCode { get; set; }
}
