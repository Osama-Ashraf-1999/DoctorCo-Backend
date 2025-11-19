using ClinicApi.DTOs;
using ClinicApi.Models;

namespace ClinicApi.Services;

public interface IScheduleService
{
    Task<Schedule> CreateAsync(CreateScheduleDto dto);
    Task<Schedule?> GetByIdAsync(Guid id);
    Task<IEnumerable<Schedule>> GetByUserAsync(Guid userId);
    Task UpdateAsync(Guid id, UpdateScheduleDto dto);
    Task DeleteAsync(Guid id);
}
