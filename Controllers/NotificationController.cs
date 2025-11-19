using ClinicApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClinicApi.DTOs;

namespace ClinicApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _svc;
    public NotificationController(INotificationService svc) { _svc = svc; }

    //[Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string message)
    {
        var n = await _svc.CreateAsync(message);
        return CreatedAtAction(nameof(GetAll), new { id = n.Id }, n);
    }

    [Authorize]
    [HttpPatch("{id}/read") ]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        await _svc.MarkAsReadAsync(id);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _svc.DeleteAsync(id);
        return NoContent();
    }
}
