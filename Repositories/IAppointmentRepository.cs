using ClinicApi.Models;

namespace ClinicApi.Repositories;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(Guid id);
    Task<IEnumerable<Appointment>> GetByUserAsync(Guid userId);
    Task AddAsync(Appointment appt);
    Task UpdateAsync(Appointment appt);
    Task DeleteAsync(Appointment appt);
}
