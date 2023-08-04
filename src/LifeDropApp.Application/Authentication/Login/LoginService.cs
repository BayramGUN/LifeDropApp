using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Requests.Authentication;
using LifeDropApp.Application.Common.DTOs.Responses.Authentication;
using LifeDropApp.Application.Services.Authentication.Extensions;
using LifeDropApp.Application.Services.Authentication.Interfaces;
using LifeDropApp.Application.Services.Authentication.Token;
using LifeDropApp.Infrastructure.Repositories.Interfaces;

namespace LifeDropApp.Application.Authentication.Login;

public class LoginService : ILoginService
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public LoginService(
        IJwtTokenGenerator tokenGenerator,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<AuthenticationResponse> Login(LoginRequest request)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username);

        if(user is null || !request.Password.VerifyPassword(user.Password))
            throw new InvalidOperationException("Credentials are not valid!");

        var token = _tokenGenerator.GenerateToken(user);
        
        return new AuthenticationResponse{
            User = user.FromUserToAuthResponse(_mapper),
            Token = token
        };
    }
}