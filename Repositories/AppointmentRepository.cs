using Microsoft.EntityFrameworkCore;
using ClinicApi.Data;
using ClinicApi.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _db;
    public AppointmentRepository(AppDbContext db)
    {
        _db = db;
    }

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
        return await _db.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Appointment>> GetByDoctorAsync(Guid doctorId)
    {
        return await _db.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.DoctorId == doctorId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByPatientAsync(Guid patientId)
    {
        return await _db.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Where(a => a.PatientId == patientId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Appointment appt)
    {
        _db.Appointments.Update(appt);
        await _db.SaveChangesAsync();
    }
}
