namespace ClinicApi.DTOs;

public class RegisterDto
{
    public string fullName { get; set; } = null!;
    public string email { get; set; } = null!;
    public string password { get; set; } = null!;
    public string? phoneNumber { get; set; }
    public string? city { get; set; }
    public string? role { get; set; }
    public string? speciality { get; set; }
    public string? location { get; set; }
    public string? image { get; set; }
    public string? bio { get; set; }
    public decimal? reservationPrice { get; set; }
    public string? gender { get; set; }

}

public class LoginDto
{
    public string email { get; set; } = null!;
    public string password { get; set; } = null!;
}
