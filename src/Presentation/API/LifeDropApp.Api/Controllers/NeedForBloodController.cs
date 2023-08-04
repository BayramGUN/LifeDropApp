using LifeDropApp.Application.Common.DTOs.Requests.NeedForBlood;
using LifeDropApp.Application.Services.NeedForBloods.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LifeDropApp.Api.Controllers;

[Route("needForBloods")]
public class NeedForBloodController : ControllerBase
{
    private readonly INeedForBloodService _needForContollerService;
    public NeedForBloodController(INeedForBloodService needForContollerService)
    {
        _needForContollerService = needForContollerService;
    }

    [Authorize("HospitalOnly")]
    [HttpPost("create")]
    public async Task<IActionResult> AddNeedForBlood([FromBody]CreateNeedForBloodRequest request)
    {
        try
        {
            await _needForContollerService.CreateNeedForBloodAsync(request);
            return Created("Created succesfully", new {Message = "Need for blood has been added succesfully!", Request = request});
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message});
        }
    }
    
    [Authorize("AdminOnly")]
    [HttpDelete("delete/{hospitalId}")]
    public async Task<IActionResult> DeleteFromHospital([FromRoute] Guid hospitalId)
    {
        try
        {
            await _needForContollerService.DeleteNeedForBloodFromHospitalAsync(hospitalId);
            return Ok(new { Message = "Delete all from hospital successfully!"});
        }
        catch (Exception exception)
        {
            
            return BadRequest(new { Message = exception.Message });
        }
    }


    [HttpPut("update")]
    public async Task<IActionResult> UpdateNeedForBlood([FromBody] UpdateNeedForBloodRequest request)
    {
        try
        {
            await _needForContollerService.UpdateNeedForBloodAsync(request);
            return Ok(request);
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message});
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetNeedForBloods() =>
        Ok(await _needForContollerService.GelAllNeedForBloodsAsync());

    [HttpGet("biggerThanZero")]
    public async Task<IActionResult> GetBiggerThanZero() =>
        Ok(await _needForContollerService.GetNeedForBloodsBiggerThanZeroAsync());

    [HttpGet("byHospital")]
    public async Task<IActionResult> GetBiggerThanZero([FromQuery] Guid hospitalId) =>
        Ok(await _needForContollerService.GetNeedForBloodsByHospitalIdAsync(hospitalId));

    [HttpGet("needForBlood")]
    public async Task<IActionResult> GetNeedForBlood([FromQuery] Guid id) =>
        Ok(await _needForContollerService.GetNeedForBloodByIdAsync(id));

    [HttpGet("{bloodType}")]
    public async Task<IActionResult> GetNeedForBloodsByBloodType(string bloodType) =>
        Ok(await _needForContollerService.GetNeedForBloodsByBloodTypeAsync(bloodType));


    [Authorize("HospitalOnly")]
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteNeedForBlood([FromQuery] Guid id)
    {
        try
        {
            await _needForContollerService.DeleteNeedForBloodAsync(id);
            return Ok(new { Message = "Need for blood announcement has been deleted succesfully"});
        }
        catch(Exception exception)
        {
            return BadRequest(new {Message = exception.Message});
        }
    }

    [Authorize("HospitalOnly")]
    [HttpPatch("quantities/{id}")]
    public async Task<IActionResult> DecreaseQuantity([FromRoute] Guid id, [FromBody] int quantityNeeded)
    {
        try
        {
            await _needForContollerService.DecreaseQuantityNeeded(id, quantityNeeded);
            return Accepted(new { Message = "Quantity of need for blood decreased successfully"});
        }
        catch(InvalidOperationException exception)
        {
            return BadRequest(new { Message = exception.Message});
        }
    }

}