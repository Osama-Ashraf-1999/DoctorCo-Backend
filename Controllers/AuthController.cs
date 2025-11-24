using ClinicApi.DTOs;
using ClinicApi.Models;
using ClinicApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    // ==================== Register ====================
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            var user = await _auth.RegisterAsync(dto);

            return Created("", new
            {
                userId = user.userId,
                fullName = user.fullName,
                email = user.email,
                role = user.role
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // ==================== Login ====================
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var token = await _auth.LoginAsync(dto);
            var user = await _auth.GetUserByEmailAsync(dto.email); // ناخد بيانات اليوزر كاملة

            return Ok(new
            {
                token,
                user = new
                {
                    user.userId,
                    user.fullName,
                    user.email,
                    user.role,
                    user.phoneNumber,
                    user.city,
                    user.speciality,
                    user.location,
                    user.bio,
                    user.image,
                    user.reservationPrice
                }
            });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
