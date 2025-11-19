using System.ComponentModel.DataAnnotations;

namespace ClinicApi.Models;

public class Notification
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
