using ClinicApi.DTOs;
using ClinicApi.Models;
using ClinicApi.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClinicApi.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepo, IConfiguration config)
    {
        _userRepo = userRepo;
        _config = config;
    }

    public async Task<User> RegisterAsync(RegisterDto dto)
    {
        var exists = await _userRepo.GetByEmailAsync(dto.Email);
        if (exists is not null) throw new Exception("Email already registered.");

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Role = dto.Role ?? "Patient",
            PhoneNumber = dto.PhoneNumber,
            City = dto.City,
            Speciality = dto.Speciality,
            Location = dto.Location,
            Bio = dto.Bio,
            Image = dto.Image,
            Reservation_Price = dto.Reservation_Price,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _userRepo.AddAsync(user);
        return user;
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userRepo.GetByEmailAsync(dto.Email);
        if (user is null) throw new Exception("Invalid credentials.");
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) throw new Exception("Invalid credentials.");

        // create token
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"] ?? "ReplaceWithStrongKeyInProduction");
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
