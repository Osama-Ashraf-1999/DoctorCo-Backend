using System;

namespace ClinicApi.DTOs;

public class CreateAppointmentDto
{
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public TimeSpan Appointment_Time { get; set; }
    public DateOnly Appointment_Date { get; set; }
    public string? Description { get; set; }
}

public class AppointmentDto
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public DateOnly AppointmentDate { get; set; }
    public TimeSpan AppointmentTime { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; } = "inProgress";
    public DateTime CreatedAt { get; set; }
}
