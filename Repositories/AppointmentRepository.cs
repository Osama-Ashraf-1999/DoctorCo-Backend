using ClinicApi.Data;
using ClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _db;
    public AppointmentRepository(AppDbContext db) { _db = db; }

    public async Task AddAsync(Appointment appt)
    {
        _db.Appointments.Add(appt);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Appointment appt)
    {
        _db.Appointments.Remove(appt);
        await _db.SaveChangesAsync();
    }

    public async Task<Appointment?> GetByIdAsync(Guid id)
    {
        return await _db.Appointments.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Appointment>> GetByUserAsync(Guid userId)
    {
        return await _db.Appointments.Where(a => a.userId == userId).ToListAsync();
    }

    public async Task UpdateAsync(Appointment appt)
    {
        _db.Appointments.Update(appt);
        await _db.SaveChangesAsync();
    }
}
