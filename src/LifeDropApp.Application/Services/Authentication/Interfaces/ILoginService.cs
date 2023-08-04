using LifeDropApp.Application.Common.DTOs.Requests.Authentication;
using LifeDropApp.Application.Common.DTOs.Responses.Authentication;

namespace LifeDropApp.Application.Services.Authentication.Interfaces;

public interface ILoginService
{
    Task<AuthenticationResponse> Login(LoginRequest request);
}