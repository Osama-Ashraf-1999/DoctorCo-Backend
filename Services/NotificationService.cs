using ClinicApi.Models;
using ClinicApi.Repositories;

namespace ClinicApi.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repo;
    public NotificationService(INotificationRepository repo) { _repo = repo; }

    public async Task<Notification> CreateAsync(string message)
    {
        var n = new Notification { Message = message };
        await _repo.AddAsync(n);
        return n;
    }

    public async Task DeleteAsync(Guid id)
    {
        var n = await _repo.GetByIdAsync(id);
        if (n is null) throw new Exception("Notification not found.");
        await _repo.DeleteAsync(n);
    }

    public async Task<IEnumerable<Notification>> GetAllAsync() => await _repo.GetAllAsync();

    public async Task MarkAsReadAsync(Guid id)
    {
        var n = await _repo.GetByIdAsync(id);
        if (n is null) throw new Exception("Notification not found.");
        n.IsRead = true;
        await _repo.UpdateAsync(n);
    }
}
