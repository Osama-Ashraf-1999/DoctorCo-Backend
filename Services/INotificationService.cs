using ClinicApi.Models;

namespace ClinicApi.Services;

public interface INotificationService
{
    Task<IEnumerable<Notification>> GetAllAsync();
    Task<Notification> CreateAsync(string message);
    Task MarkAsReadAsync(Guid id);
    Task DeleteAsync(Guid id);
}
