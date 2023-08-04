using System.ComponentModel.DataAnnotations;

namespace LifeDropApp.Application.Common.DTOs.Requests.User;

public class UpdateUserRequest
{

    [Required]
    public Guid Id { get; set; }
    [Required]
    [MinLength(3)]
    public string Username { get; set; } = null!;
    [Required]
    [MinLength(11)]
    public string Phone { get; set; } = null!;
    public string? Email { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = null!;
    [Required]
    [MinLength(3)]
    public string Role { get; set; } = null!;
}