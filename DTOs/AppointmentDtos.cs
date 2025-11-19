using System;

namespace ClinicApi.DTOs;

public class CreateAppointmentDto
{
    public Guid UserId { get; set; }
    public TimeSpan Appointment_Time { get; set; }
    public DateOnly Appointment_Date { get; set; }
}
