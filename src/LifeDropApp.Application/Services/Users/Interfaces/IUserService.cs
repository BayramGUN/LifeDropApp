using LifeDropApp.Application.Common.DTOs.Requests;
using LifeDropApp.Application.Common.DTOs.Requests.User;
using LifeDropApp.Application.Common.DTOs.Responses.User;

namespace LifeDropApp.Application.Services.Users.Interfaces;

public interface IUserService
{
    Task DeleteUserAsync(Guid id);
    Task<IEnumerable<UserResponse?>> GelAllUsersAsync();
    Task UpdateUserAsync(UpdateUserRequest updateUserRequest);
    Task<UserResponse> GetUserById(Guid id);
    Task<UserResponse> GetUserByUsername(string username);
    Task<UserResponse> GetUserByUserEmail(string email);
}