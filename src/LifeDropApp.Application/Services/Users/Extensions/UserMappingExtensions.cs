using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Requests;
using LifeDropApp.Application.Common.DTOs.Requests.User;
using LifeDropApp.Application.Common.DTOs.Responses.User;
using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Application.Services.Users.Extensions;
public static class UsersMappingExtensions
{
    public static IEnumerable<UserResponse> FromUserToUserResponses(this IList<User> users, IMapper mapper) =>
        mapper.Map<IEnumerable<UserResponse>>(users);
    public static UserResponse FromUserToUserResponse(this User users, IMapper mapper) =>
        mapper.Map<UserResponse>(users);
    public static User FromUpdateUserRequestToUser(this UpdateUserRequest userUpdate, IMapper mapper) =>
        mapper.Map<User>(userUpdate);
}

