using ClinicApi.DTOs;
using ClinicApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _svc;
    public AppointmentController(IAppointmentService svc) { _svc = svc; }

    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentDto dto)
    {
        try
        {
            var a = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = a.Id }, a);
        }
        catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
    }

    //[Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var a = await _svc.GetByIdAsync(id);
        if (a is null) return NotFound();
        return Ok(a);
    }

    //[Authorize]
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(Guid userId)
    {
        var list = await _svc.GetByUserAsync(userId);
        return Ok(list);
    }

   // [Authorize]
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] string status)
    {
        try
        {
            await _svc.UpdateStatusAsync(id, status);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
    }

   // [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _svc.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
    }

    // [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _svc.GetAllAsync();
        return Ok(list);
    }
}
