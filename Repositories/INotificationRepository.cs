using ClinicApi.Models;

namespace ClinicApi.Repositories;

public interface INotificationRepository
{
    Task<Notification?> GetByIdAsync(Guid id);
    Task<IEnumerable<Notification>> GetAllAsync();
    Task AddAsync(Notification note);
    Task UpdateAsync(Notification note);
    Task DeleteAsync(Notification note);
}
