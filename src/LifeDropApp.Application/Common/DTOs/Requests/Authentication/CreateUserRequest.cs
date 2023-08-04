using System.ComponentModel.DataAnnotations;

namespace LifeDropApp.Application.Common.DTOs.Requests.Authentication;

public class CreateUserRequest
{
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