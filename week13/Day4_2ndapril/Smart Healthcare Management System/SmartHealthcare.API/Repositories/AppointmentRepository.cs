using Microsoft.EntityFrameworkCore;
using SmartHealthcare.API.Data;
using SmartHealthcare.API.Repositories.Interfaces;
using SmartHealthcare.Core.Models;

namespace SmartHealthcare.API.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _db;
        public AppointmentRepository(AppDbContext db) { _db = db; }

        private IQueryable<Appointment> WithIncludes() =>
            _db.Appointments.Include(a => a.Patient).Include(a => a.Doctor);

        public async Task<IEnumerable<Appointment>> GetAllAsync() =>
            await WithIncludes().ToListAsync();

        public async Task<IEnumerable<Appointment>> GetByDateAsync(DateTime date) =>
            await WithIncludes().Where(a => a.AppointmentDate.Date == date.Date).ToListAsync();

        public async Task<IEnumerable<Appointment>> GetByPatientAsync(int patientId) =>
            await WithIncludes().Where(a => a.PatientId == patientId).ToListAsync();

        public async Task<IEnumerable<Appointment>> GetByDoctorAsync(int doctorId) =>
            await WithIncludes().Where(a => a.DoctorId == doctorId).ToListAsync();

        public async Task<IEnumerable<Appointment>> GetByDoctorUserIdAsync(int userId) =>
            await WithIncludes().Where(a => a.Doctor.UserId == userId).ToListAsync();

        public async Task<Appointment?> GetByIdAsync(int id) =>
            await WithIncludes().FirstOrDefaultAsync(a => a.Id == id);

        public async Task<Appointment> CreateAsync(Appointment appt)
        {
            _db.Appointments.Add(appt);
            await _db.SaveChangesAsync();
            return appt;
        }

        public async Task<Appointment?> UpdateStatusAsync(int id, string status)
        {
            var a = await _db.Appointments.FindAsync(id);
            if (a == null) return null;
            a.Status = status;
            await _db.SaveChangesAsync();
            return a;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var a = await _db.Appointments.FindAsync(id);
            if (a == null) return false;
            _db.Appointments.Remove(a);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}

