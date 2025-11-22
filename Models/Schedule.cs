using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicApi.Models;

public class Schedule
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [ForeignKey("User")]
    public Guid userId { get; set; }
    public User? User { get; set; }
    public int Day_Of_Week { get; set; } // 1=Monday ..7=Sunday
    public TimeSpan Start_Time { get; set; }
    public TimeSpan End_Time { get; set; }
    public bool IsAvailable { get; set; } = true;
}
