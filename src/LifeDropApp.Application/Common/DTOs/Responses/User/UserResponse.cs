using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Application.Common.DTOs.Responses.User;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public IList<string>? AdminName { get; set; }
    public IList<string>? DonorName { get; set; }
    public IList<string>? HospitalName { get; set; }
} 