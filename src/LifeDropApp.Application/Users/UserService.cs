using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Responses.User;
using LifeDropApp.Application.Services.Users.Interfaces;
using LifeDropApp.Application.Services.Users.Extensions;
using LifeDropApp.Infrastructure.Repositories.Interfaces;
using LifeDropApp.Application.Common.DTOs.Requests.User;

namespace LifeDropApp.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        if(!await _userRepository.IsExistsAsync(id))
            throw new ArgumentNullException($"There is no user with {id}");
        
        await _userRepository.RemoveAsync(id);
    }

    public async Task<IEnumerable<UserResponse?>> GelAllUsersAsync() 
    {
        var users = await _userRepository.GetAllAsync();
        return users.FromUserToUserResponses(_mapper);
    }

    public async Task<UserResponse> GetUserById(Guid id)
    {
        var user = await _userRepository.GetAsync(id);
        return user!.FromUserToUserResponse(_mapper);
    }

    public async Task<UserResponse> GetUserByUserEmail(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        return user!.FromUserToUserResponse(_mapper);
    }

    public async Task<UserResponse> GetUserByUsername(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        return user!.FromUserToUserResponse(_mapper);
    }

    public async Task UpdateUserAsync(UpdateUserRequest request)
    {
        if(!await _userRepository.IsExistsAsync(request.Id))
            throw new ArgumentNullException($"There is no user with {request.Id}");
        var userUpdated = request.FromUpdateUserRequestToUser(_mapper);
        await _userRepository.UpdateAsync(userUpdated);
    }
}