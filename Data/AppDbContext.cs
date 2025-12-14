using ClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<Schedule> Schedules { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /* ===============================
           DateOnly Conversion
        =============================== */
        modelBuilder.Entity<Appointment>()
            .Property(a => a.Appointment_Date)
            .HasConversion(
                d => d.ToDateTime(TimeOnly.MinValue),
                d => DateOnly.FromDateTime(d)
            );

        /* ===============================
           Doctor Relationship
        =============================== */
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany() // مفيش Navigation فى User
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        /* ===============================
           Patient Relationship
        =============================== */
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany() // مفيش Navigation فى User
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
