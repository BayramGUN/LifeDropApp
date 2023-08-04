using LifeDropApp.Application.Common.DTOs.Requests.Authentication;
using LifeDropApp.Application.Common.DTOs.Responses.Authentication;

namespace LifeDropApp.Application.Services.Authentication.Interfaces;

public interface IRegisterService
{
    Task<AuthenticationResponse> Register(CreateUserRequest request);
}