using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Requests.Authentication;
using LifeDropApp.Application.Common.DTOs.Responses.User;
using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Application.Services.Authentication.Extensions;
public static class AuthenticationMappingExtensions
{
    public static User FromCreateUserRequestToUser(this CreateUserRequest userRequest, IMapper mapper) =>
        mapper.Map<User>(userRequest);
    public static UserResponse FromUserToAuthResponse(this User user, IMapper mapper) =>
        mapper.Map<UserResponse>(user);
}

