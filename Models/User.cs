using System.ComponentModel.DataAnnotations;

namespace ClinicApi.Models;

public enum Gender { Male, Female, Other }

public class User
{
    [Key]
    public Guid UserId { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Gender? Gender { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? City { get; set; }
    public string Role { get; set; } = "Patient"; // Patient or Doctors
    public string? Speciality { get; set; }
    public string? Location { get; set; }
    public string? Image { get; set; }
    public string? Bio { get; set; }
    public decimal? Reservation_Price { get; set; }
}
