using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LifeDropApp.Application.Common.DTOs.Requests.Hospital;
using LifeDropApp.Application.Services.Hospitals.Interfaces;
using System.Security.Claims;
using System.Diagnostics;

namespace SurveyManager.Api.Controllers;

[Route("hospitals")]
public class HospitalController : ControllerBase
{
    private readonly IHospitalService _hospitalService;
    public HospitalController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    [Authorize("AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAllHospitals()
    {
        return Ok(await _hospitalService.GetAllHospitalsAsync());
    }

    [Authorize(Roles = "admin, hospital")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateHospital([FromBody]CreateHospitalRequest hospitalRequest)
    {
        try
        {
          /*   var user = HttpContext.User;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            var userRole = string.Join(", ", roles);
            Debug.WriteLine(roles.ToString()); */
            await _hospitalService.CreateHospitalAsync(hospitalRequest);
            
            return Created("Hospital created succesfully", new { Name = hospitalRequest.Name });
        }
        catch(Exception exception)
        {
            return BadRequest(new {Message= exception.Message});
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateHospital(
        [FromBody] UpdateHospitalRequest hospitalRequest,
        [FromRoute] Guid id)
    {
        hospitalRequest.Id = id;
        if(ModelState.IsValid)
        {
            await _hospitalService.UpdateHospitalAsync(hospitalRequest);
            return Created("Hospital updated succesfully", new { Name = hospitalRequest.Name });
        }
        else
            return BadRequest("Invalid model states!");
    }

    [Authorize("AdminOnly")]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _hospitalService.DeleteHospitalAsync(id);
            return Ok($"{id} user was deleted!");
        }
        catch(Exception exception)
        {   
            return BadRequest(new { Message = exception.Message });
        }
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetHospitalsById([FromRoute] Guid id) =>
        Ok(await _hospitalService.GetHospital(id));
    [HttpGet("get/users/{userId}")]
    public async Task<IActionResult> GetHospitalByUserId([FromRoute] Guid userId) =>
        Ok(await _hospitalService.GetHospitalByUserAsync(userId));
}