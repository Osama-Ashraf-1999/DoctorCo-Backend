using ClinicApi.DTOs;
using ClinicApi.Models;
using ClinicApi.Repositories;

namespace ClinicApi.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _repo;
    private readonly IUserRepository _userRepo;

    public ScheduleService(IScheduleRepository repo, IUserRepository userRepo)
    {
        _repo = repo;
        _userRepo = userRepo;
    }

    public async Task<Schedule> CreateAsync(CreateScheduleDto dto)
    {
        var user = await _userRepo.GetByIdAsync(dto.UserId);
        if (user is null) throw new Exception("User not found.");

        if (dto.End_Time <= dto.Start_Time) throw new Exception("End time must be after start time.");
        if (dto.Day_Of_Week < 1 || dto.Day_Of_Week > 7) throw new Exception("Day of week must be 1..7.");

        var s = new Schedule
        {
            UserId = dto.UserId,
            Day_Of_Week = dto.Day_Of_Week,
            Start_Time = dto.Start_Time,
            End_Time = dto.End_Time,
            IsAvailable = dto.IsAvailable
        };

        await _repo.AddAsync(s);
        return s;
    }

    public async Task DeleteAsync(Guid id)
    {
        var s = await _repo.GetByIdAsync(id);
        if (s is null) throw new Exception("Schedule not found.");
        await _repo.DeleteAsync(s);
    }

    public async Task<Schedule?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

    public async Task<IEnumerable<Schedule>> GetByUserAsync(Guid userId) => await _repo.GetByUserAsync(userId);

    public async Task UpdateAsync(Guid id, UpdateScheduleDto dto)
    {
        var s = await _repo.GetByIdAsync(id);
        if (s is null) throw new Exception("Schedule not found.");
        s.Day_Of_Week = dto.Day_Of_Week;
        s.Start_Time = dto.Start_Time;
        s.End_Time = dto.End_Time;
        s.IsAvailable = dto.IsAvailable;
        await _repo.UpdateAsync(s);
    }
}
