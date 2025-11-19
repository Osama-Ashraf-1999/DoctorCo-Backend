namespace ClinicApi.DTOs;

public class RegisterDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? City { get; set; }
    public string? Role { get; set; }
    public string? Speciality { get; set; }
    public string? Location { get; set; }
    public string? Image { get; set; }
    public string? Bio { get; set; }
    public decimal? Reservation_Price { get; set; }
}

public class LoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
