using LifeDropApp.Application.Common.DTOs.Responses.User;

namespace LifeDropApp.Application.Common.DTOs.Responses.Authentication;

public class AuthenticationResponse
{
    public UserResponse User { get; set; } = null!;
    public string Token { get; set; } = null!;
} 