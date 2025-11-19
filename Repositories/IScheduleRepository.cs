using ClinicApi.Models;

namespace ClinicApi.Repositories;

public interface IScheduleRepository
{
    Task<Schedule?> GetByIdAsync(Guid id);
    Task<IEnumerable<Schedule>> GetByUserAsync(Guid userId);
    Task AddAsync(Schedule schedule);
    Task UpdateAsync(Schedule schedule);
    Task DeleteAsync(Schedule schedule);
}
