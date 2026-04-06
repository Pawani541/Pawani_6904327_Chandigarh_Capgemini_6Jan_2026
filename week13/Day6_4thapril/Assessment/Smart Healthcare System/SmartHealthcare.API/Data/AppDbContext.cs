using Microsoft.EntityFrameworkCore;
using SmartHealthcare.API.Models;

namespace SmartHealthcare.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("HospitalUsers");
            modelBuilder.Entity<Department>().ToTable("HospitalDepartments");
            modelBuilder.Entity<Doctor>().ToTable("HospitalDoctors");
            modelBuilder.Entity<Appointment>().ToTable("HospitalAppointments");
            modelBuilder.Entity<Prescription>().ToTable("HospitalPrescriptions");
            modelBuilder.Entity<Bill>().ToTable("HospitalBills");

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne(u => u.Doctor)
                .HasForeignKey<Doctor>(d => d.UserId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Department)
                .WithMany(dep => dep.Doctors)
                .HasForeignKey(d => d.DepartmentId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Appointment)
                .WithOne(a => a.Prescription)
                .HasForeignKey<Prescription>(p => p.AppointmentId);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Appointment)
                .WithOne(a => a.Bill)
                .HasForeignKey<Bill>(b => b.AppointmentId);

            modelBuilder.Entity<Bill>()
                .Property(b => b.ConsultationFee)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Bill>()
                .Property(b => b.MedicineCharges)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Bill>()
                .Ignore(b => b.TotalAmount);

            modelBuilder.Entity<Prescription>()
                .Property(p => p.MedicineCharges)
                .HasColumnType("decimal(10,2)");
        }
    }
}
