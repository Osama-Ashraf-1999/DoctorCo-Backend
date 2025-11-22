using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicApi.Models;

public class Appointment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [ForeignKey("User")]
    public Guid userId { get; set; }
    public User? User { get; set; }
    public TimeSpan Appointment_Time { get; set; }
    public DateOnly Appointment_Date { get; set; }
    public string Status { get; set; } = "pending"; // pending-canceled-confirmed-deleted
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
}
