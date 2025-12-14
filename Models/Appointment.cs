using ClinicApi.Models;
using System.ComponentModel.DataAnnotations;

public class Appointment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    // Doctor
    public Guid DoctorId { get; set; }
    public User Doctor { get; set; } = null!;

    // Patient
    public Guid PatientId { get; set; }
    public User Patient { get; set; } = null!;

    public TimeSpan Appointment_Time { get; set; }
    public DateOnly Appointment_Date { get; set; }

    // pending, canceled, confirmed, deleted
    public string Status { get; set; } = "inProgress";

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
