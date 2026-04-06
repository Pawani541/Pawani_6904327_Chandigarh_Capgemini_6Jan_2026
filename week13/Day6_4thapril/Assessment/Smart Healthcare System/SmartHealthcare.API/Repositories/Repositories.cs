using Microsoft.EntityFrameworkCore;
using SmartHealthcare.API.Data;
using SmartHealthcare.API.Interfaces;
using SmartHealthcare.API.Models;

namespace SmartHealthcare.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();
        public async Task<User?> GetByIdAsync(int id) => await _context.Users.FindAsync(id);
        public async Task<User?> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        public async Task AddAsync(User user) { _context.Users.Add(user); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(User user) { _context.Users.Update(user); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null) { _context.Users.Remove(user); await _context.SaveChangesAsync(); }
        }
    }

    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;
        public DoctorRepository(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<Doctor>> GetAllAsync() =>
            await _context.Doctors.Include(d => d.User).Include(d => d.Department).ToListAsync();
        public async Task<Doctor?> GetByIdAsync(int id) =>
            await _context.Doctors.Include(d => d.User).Include(d => d.Department).FirstOrDefaultAsync(d => d.DoctorId == id);
        public async Task<IEnumerable<Doctor>> GetByDepartmentAsync(int deptId) =>
            await _context.Doctors.Include(d => d.User).Where(d => d.DepartmentId == deptId).ToListAsync();
        public async Task AddAsync(Doctor doctor) { _context.Doctors.Add(doctor); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Doctor doctor) { _context.Doctors.Update(doctor); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var doc = await _context.Doctors.FindAsync(id);
            if (doc != null) { _context.Doctors.Remove(doc); await _context.SaveChangesAsync(); }
        }
    }

    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;
        public AppointmentRepository(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<Appointment>> GetAllAsync() =>
            await _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ThenInclude(d => d.User).ToListAsync();
        public async Task<Appointment?> GetByIdAsync(int id) =>
            await _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ThenInclude(d => d.User).FirstOrDefaultAsync(a => a.AppointmentId == id);
        public async Task<IEnumerable<Appointment>> GetByPatientAsync(int patientId) =>
            await _context.Appointments.Include(a => a.Doctor).Where(a => a.PatientId == patientId).ToListAsync();
        public async Task AddAsync(Appointment appt) { _context.Appointments.Add(appt); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Appointment appt) { _context.Appointments.Update(appt); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var appt = await _context.Appointments.FindAsync(id);
            if (appt != null) { _context.Appointments.Remove(appt); await _context.SaveChangesAsync(); }
        }
    }

    public class BillRepository : IBillRepository
    {
        private readonly AppDbContext _context;
        public BillRepository(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<Bill>> GetAllAsync() =>
            await _context.Bills.Include(b => b.Appointment).ToListAsync();
        public async Task<Bill?> GetByIdAsync(int id) => await _context.Bills.FindAsync(id);
        public async Task<Bill?> GetByAppointmentAsync(int appointmentId) =>
            await _context.Bills.FirstOrDefaultAsync(b => b.AppointmentId == appointmentId);
        public async Task AddAsync(Bill bill) { _context.Bills.Add(bill); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Bill bill) { _context.Bills.Update(bill); await _context.SaveChangesAsync(); }
    }
}
