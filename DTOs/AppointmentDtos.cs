using System;

namespace ClinicApi.DTOs;

public class CreateAppointmentDto
{
    public Guid userId { get; set; }
    public TimeSpan Appointment_Time { get; set; }
    public DateOnly Appointment_Date { get; set; }
}
