using ClinicApi.Data;
using ClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly AppDbContext _db;
    public NotificationRepository(AppDbContext db) { _db = db; }

    public async Task AddAsync(Notification note)
    {
        _db.Notifications.Add(note);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Notification note)
    {
        _db.Notifications.Remove(note);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Notification>> GetAllAsync()
    {
        return await _db.Notifications.OrderByDescending(n => n.createdAt).ToListAsync();
    }

    public async Task<Notification?> GetByIdAsync(Guid id)
    {
        return await _db.Notifications.FindAsync(id);
    }

    public async Task UpdateAsync(Notification note)
    {
        _db.Notifications.Update(note);
        await _db.SaveChangesAsync();
    }
}
