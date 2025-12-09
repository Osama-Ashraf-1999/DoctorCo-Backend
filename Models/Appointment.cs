using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicApi.Models;

public class Appointment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    // Doctor
    [ForeignKey("Doctor")]
    public Guid DoctorId { get; set; }
    public User? Doctor { get; set; }

    // Patient
    [ForeignKey("Patient")]
    public Guid PatientId { get; set; }
    public User? Patient { get; set; }

    public TimeSpan Appointment_Time { get; set; }
    public DateOnly Appointment_Date { get; set; }

    // status: pending, canceled, confirmed, deleted
    public string Status { get; set; } = "pending";

    // description
    public string? Description { get; set; }

    public DateTime createdAt { get; set; } = DateTime.UtcNow;
}
