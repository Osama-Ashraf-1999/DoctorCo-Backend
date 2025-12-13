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
        var patient = await _userRepo.GetByIdAsync(dto.PatientId);
        if (patient is null) throw new Exception("Patient not found.");

        var doctor = await _userRepo.GetByIdAsync(dto.DoctorId);
        if (doctor is null) throw new Exception("Doctor not found.");

        if (dto.Appointment_Date < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new Exception("Appointment date cannot be in the past.");

        var appt = new Appointment
        {
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            Appointment_Date = dto.Appointment_Date,
            Appointment_Time = dto.Appointment_Time,
            Status = "inProgress",
            Description = dto.Description
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

    public async Task<IEnumerable<Appointment>> GetByUserAsync(Guid userId)
    {
        var asPatient = await _repo.GetByPatientAsync(userId);
        var asDoctor = await _repo.GetByDoctorAsync(userId);
        return asPatient.Concat(asDoctor);
    }

    public async Task UpdateStatusAsync(Guid id, string status)
    {
        var a = await _repo.GetByIdAsync(id);
        if (a is null) throw new Exception("Appointment not found.");

        var allowed = new[] { "inProgress", "canceled", "confirmed", "deleted" };
        if (!allowed.Contains(status)) throw new Exception("Invalid status.");

        a.Status = status;
        await _repo.UpdateAsync(a);
    }
}
