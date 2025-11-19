using ClinicApi.Data;
using ClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly AppDbContext _db;
    public ScheduleRepository(AppDbContext db) { _db = db; }

    public async Task AddAsync(Schedule schedule)
    {
        _db.Schedules.Add(schedule);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Schedule schedule)
    {
        _db.Schedules.Remove(schedule);
        await _db.SaveChangesAsync();
    }

    public async Task<Schedule?> GetByIdAsync(Guid id)
    {
        return await _db.Schedules.FindAsync(id);
    }

    public async Task<IEnumerable<Schedule>> GetByUserAsync(Guid userId)
    {
        return await _db.Schedules.Where(s => s.UserId == userId).ToListAsync();
    }

    public async Task UpdateAsync(Schedule schedule)
    {
        _db.Schedules.Update(schedule);
        await _db.SaveChangesAsync();
    }
}
