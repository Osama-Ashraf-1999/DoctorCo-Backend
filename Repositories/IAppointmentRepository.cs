using ClinicApi.DTOs;
using ClinicApi.Models;

namespace ClinicApi.Repositories;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(Guid id);
    Task<IEnumerable<Appointment>> GetByDoctorAsync(Guid doctorId);
    Task<IEnumerable<Appointment>> GetByPatientAsync(Guid patientId);
    Task AddAsync(Appointment appt);
    Task UpdateAsync(Appointment appt);
    Task DeleteAsync(Appointment appt);
    Task<IEnumerable<AppointmentDto>> GetAllAsync();
}
