using ClinicApi.DTOs;
using ClinicApi.Models;

namespace ClinicApi.Services;

public interface IAppointmentService
{
    Task<Appointment> CreateAsync(CreateAppointmentDto dto);
    Task<Appointment?> GetByIdAsync(Guid id);
    Task<IEnumerable<Appointment>> GetByUserAsync(Guid userId);
    Task UpdateStatusAsync(Guid id, string status);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<AppointmentDto>> GetAllAsync();
}
