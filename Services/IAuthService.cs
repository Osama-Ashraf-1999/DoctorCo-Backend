using ClinicApi.DTOs;
using ClinicApi.Models;

namespace ClinicApi.Services;

public interface IAuthService
{
    Task<User> RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
    Task GetUserByEmail(string email);
}
