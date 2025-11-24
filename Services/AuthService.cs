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
        var exists = await _userRepo.GetByEmailAsync(dto.email);
        if (exists is not null) throw new Exception("Email already registered.");

        var user = new User
        {
            fullName = dto.fullName,
            email = dto.email,
            role = dto.role ?? "patient",
            phoneNumber = dto.phoneNumber,
            city = dto.city,
            speciality = dto.speciality,
            location = dto.location,
            bio = dto.bio,
            image = dto.image,
            reservationPrice = dto.reservationPrice,
            passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.password)
        };

        await _userRepo.AddAsync(user);
        return user;
    }
    
    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userRepo.GetByEmailAsync(dto.email);
        if (user is null) throw new Exception("Invalid credentials.");
        if (!BCrypt.Net.BCrypt.Verify(dto.password, user.passwordHash))
            throw new Exception("Invalid credentials.");

        // إنشاء التوكن
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"] ?? "ReplaceWithStrongKeyInProduction");
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, user.role)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    // public Task GetUserByEmail(string email)
    // {
    //     throw new NotImplementedException();
    //  }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userRepo.GetByEmailAsync(email);
    }
}
