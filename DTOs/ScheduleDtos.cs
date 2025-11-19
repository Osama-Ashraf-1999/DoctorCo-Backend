using System;

namespace ClinicApi.DTOs;

public class CreateScheduleDto
{
    public Guid UserId { get; set; }
    public int Day_Of_Week { get; set; }
    public TimeSpan Start_Time { get; set; }
    public TimeSpan End_Time { get; set; }
    public bool IsAvailable { get; set; } = true;
}

public class UpdateScheduleDto : CreateScheduleDto { }
