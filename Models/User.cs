using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicApi.Models;

public enum Gender { Male, Female, Other }

public class User
{
    [Key]
    public Guid userId { get; set; } = Guid.NewGuid();
    public string fullName { get; set; } = null!;
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    public Gender? gender { get; set; }
    public string email { get; set; } = null!;
    public string passwordHash { get; set; } = null!;
    public string? phoneNumber { get; set; }
    public string? city { get; set; }
    public string role { get; set; } = "patient"; // patient or doctor
    public string? speciality { get; set; }
    public string? location { get; set; }
    public string? image { get; set; }
    public string? bio { get; set; }
    public decimal? reservationPrice { get; set; }
}
