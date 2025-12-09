using System;

namespace ClinicApi.DTOs;

public class CreateAppointmentDto
{
    public Guid DoctorId { get; set; }      // doc id
    public Guid PatientId { get; set; }     // patient id
    public TimeSpan Appointment_Time { get; set; }
    public DateOnly Appointment_Date { get; set; }
    public string? Description { get; set; } // additional description
}
