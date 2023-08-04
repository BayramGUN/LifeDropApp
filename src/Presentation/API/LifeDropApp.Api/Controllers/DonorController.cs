using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LifeDropApp.Application.Common.DTOs.Requests.Donor;
using LifeDropApp.Application.Services.Donors.Interfaces;

namespace SurveyManager.Api.Controllers;

[Route("donors")]
public class DonorController : ControllerBase
{
    private readonly IDonorService _donorService;
    public DonorController(IDonorService donorService)
    {
        _donorService = donorService;
    }

    [Authorize("AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAllDonors()
    {
        return Ok(await _donorService.GetAllDonorsAsync());
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateDonor([FromBody]CreateDonorRequest donorRequest)
    {
        try
        {
            await _donorService.CreateDonorAsync(donorRequest);
            return Created("Donor created succesfully", new { Name = $"{donorRequest.Firstname} {donorRequest.Lastname}" });
        }
        catch(Exception exception)
        {
            return BadRequest(new { Message = exception.Message });
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateDonor(
        [FromBody] UpdateDonorRequest donorRequest)
    {
        if(ModelState.IsValid)
        {
            await _donorService.UpdateDonorAsync(donorRequest);
            return Ok("Donor updated succesfully");
        }
        else
            return BadRequest("Invalid model states!");
    }
    [HttpPatch("donor")]
    public async Task<IActionResult> AddPoint([FromQuery] Guid id, [FromBody] bool isDonate)
    {
        if(ModelState.IsValid)
        {
            await _donorService.AddPointToDonor(id, isDonate);
            return Ok("Add point to donor succesfully");
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
            await _donorService.DeleteDonorAsync(id);
        }
        catch(ArgumentNullException exception)
        {   
            return BadRequest(exception.Message);
        }
        return Ok($"{id} user was deleted!");
    } 

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetDonorById([FromRoute] Guid id)
    {
        return Ok(await _donorService.GetDonor(id));
    } 

    [Authorize("DonorOnly")]
    [HttpGet("get/users/{userId}")]
    public async Task<IActionResult> GetDonorByUserId([FromRoute] Guid userId)
    {
        return Ok(await _donorService.GetDonorByUserId(userId));
    } 
}