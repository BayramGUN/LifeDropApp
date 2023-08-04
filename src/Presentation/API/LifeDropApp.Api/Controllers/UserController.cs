using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LifeDropApp.Application.Services.Authentication.Interfaces;
using LifeDropApp.Application.Common.DTOs.Requests;
using LifeDropApp.Application.Services.Users.Interfaces;
using LifeDropApp.Infrastructure.Repositories.Interfaces;
using LifeDropApp.Domain.Entities;
using LifeDropApp.Application.Common.DTOs.Requests.Admin;

namespace SurveyManager.Api.Controllers;

[Authorize("AdminOnly")]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _userService.GelAllUsersAsync());
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
        }
        catch(ArgumentNullException exception)
        {   
            return BadRequest(exception.Message);
        }
        return Ok($"{id} user was deleted!");
    } 
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }
        catch(ArgumentNullException exception)
        {   
            return BadRequest(exception.Message);
        }
    } 
}