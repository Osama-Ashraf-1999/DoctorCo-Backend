using ClinicApi.DTOs;
using ClinicApi.Models;
using ClinicApi.Repositories;

namespace ClinicApi.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repo;
    private readonly IUserRepository _userRepo;

    public AppointmentService(IAppointmentRepository repo, IUserRepository userRepo)
    {
        _repo = repo;
        _userRepo = userRepo;
    }

    public async Task<Appointment> CreateAsync(CreateAppointmentDto dto)
    {
        var user = await _userRepo.GetByIdAsync(dto.UserId);
        if (user is null) throw new Exception("User not found.");

        // Simple rule: appointment date must be >= today
        if (dto.Appointment_Date < DateOnly.FromDateTime(DateTime.UtcNow)) throw new Exception("Appointment date cannot be in the past.");

        var appt = new Appointment
        {
            UserId = dto.UserId,
            Appointment_Date = dto.Appointment_Date,
            Appointment_Time = dto.Appointment_Time,
            Status = "pending"
        };

        await _repo.AddAsync(appt);
        return appt;
    }

    public async Task DeleteAsync(Guid id)
    {
        var a = await _repo.GetByIdAsync(id);
        if (a is null) throw new Exception("Appointment not found.");
        await _repo.DeleteAsync(a);
    }

    public async Task<Appointment?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

    public async Task<IEnumerable<Appointment>> GetByUserAsync(Guid userId) => await _repo.GetByUserAsync(userId);

    public async Task UpdateStatusAsync(Guid id, string status)
    {
        var a = await _repo.GetByIdAsync(id);
        if (a is null) throw new Exception("Appointment not found.");
        // allowed statuses
        var allowed = new[] { "pending", "canceled", "confirmed", "deleted" };
        if (!allowed.Contains(status)) throw new Exception("Invalid status.");
        a.Status = status;
        await _repo.UpdateAsync(a);
    }
}
