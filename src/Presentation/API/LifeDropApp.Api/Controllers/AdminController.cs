using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LifeDropApp.Application.Common.DTOs.Requests.Admin;
using LifeDropApp.Application.Services.Admins.Interfaces;

namespace SurveyManager.Api.Controllers;

[Route("admins")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAdmins()
    {
        return Ok(await _adminService.GetAllAdminsAsync());
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAdmin([FromBody]CreateAdminRequest adminRequest)
    {
        await _adminService.CreateAdminAsync(adminRequest);
        return Created("Admin created succesfully", new { Name = adminRequest.Name });
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAdmin(
        [FromBody] UpdateAdminRequest adminRequest,
        [FromRoute] int id)
    {
        adminRequest.Id = id;
        if(ModelState.IsValid)
        {
            await _adminService.UpdateAdminAsync(adminRequest);
            return Created("Admin updated succesfully", new { Name = adminRequest.Name });
        }
        else
            return BadRequest("Invalid model states!");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _adminService.DeleteAdminAsync(id);
        }
        catch(ArgumentNullException exception)
        {   
            return BadRequest(exception.Message);
        }
        return Ok($"{id} user was deleted!");
    } 

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetAllAdminsById([FromRoute] int id)
    {
        return Ok(await _adminService.GetAdmin(id));
    } 
}