using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Requests.Authentication;
using LifeDropApp.Application.Common.DTOs.Responses.Authentication;
using LifeDropApp.Application.Services.Authentication.Extensions;
using LifeDropApp.Application.Services.Authentication.Interfaces;
using LifeDropApp.Application.Services.Authentication.Token;
using LifeDropApp.Infrastructure.Repositories.Interfaces;

namespace LifeDropApp.Application.Authentication.Register;

public class RegisterService : IRegisterService
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RegisterService(
        IJwtTokenGenerator tokenGenerator,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<AuthenticationResponse> Register(CreateUserRequest request)
    {
        if(await _userRepository.GetUserByUsernameAsync(request.Username) is not null) 
            throw new Exception($"{request.Username} is already exists");
        if(await _userRepository.GetUserByPhoneAsync(request.Phone) is not null)
            throw new Exception($"{request.Phone} is already exists");
        else if(await _userRepository.GetUserByEmailAsync(request.Email!) is not null)
            throw new Exception($"{request.Email} is already exists");
        
        var user = request.FromCreateUserRequestToUser(_mapper);

        await _userRepository.AddAsync(user);

        var token = _tokenGenerator.GenerateToken(user);
        
        return new AuthenticationResponse {
            Token = token,
            User = user.FromUserToAuthResponse(_mapper)
        };
    }
}