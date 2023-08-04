using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LifeDropApp.Application.Services.Authentication.Interfaces;
using LifeDropApp.Application.Common.DTOs.Requests.Authentication;

namespace SurveyManager.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly IRegisterService _registerService;

    public AuthenticationController(
        ILoginService loginService,
        IRegisterService registerService)
    {
        _loginService = loginService;
        _registerService = registerService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]CreateUserRequest registerRequest)
    {
        try 
        {
            if(ModelState.IsValid)
            {
                var authenticationResponse = await _registerService.Register(registerRequest);
                return Created($"{authenticationResponse.User.Id} Created Succesfully", authenticationResponse);
            }
            else 
                return BadRequest(new {Message = "Model states are not valid"});

        }catch(Exception exception)
        {
            return BadRequest(new {Message = exception.Message});
        }
    }

    

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest)
    {
        try
        {
            var authenticationResponse = await _loginService.Login(loginRequest);
            return Ok(authenticationResponse);
        }
        catch(InvalidOperationException exception)
        {
            return BadRequest(new { Message = exception.Message });
        }
    } 
}