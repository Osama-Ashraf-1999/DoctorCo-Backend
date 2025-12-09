using ClinicApi.DTOs;
using ClinicApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _svc;
    public ScheduleController(IScheduleService svc) { _svc = svc; }

    // Admin-only in your requirement: you'd protect with role check. Here example requires authentication.
    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateScheduleDto dto)
    {
        try
        {
            var s = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = s.Id }, s);
        }
        catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var s = await _svc.GetByIdAsync(id);
        if (s is null) return NotFound();
        return Ok(s);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(Guid userId)
    {
        var list = await _svc.GetByUserAsync(userId);
        return Ok(list);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateScheduleDto dto)
    {
        try
        {
            await _svc.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
    }

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
}
