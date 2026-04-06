using Microsoft.EntityFrameworkCore;
using SmartHealthcare.API.Data;
using SmartHealthcare.API.Repositories.Interfaces;
using SmartHealthcare.Core.Models;

namespace SmartHealthcare.API.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _db;
        public DoctorRepository(AppDbContext db) { _db = db; }

        private IQueryable<Doctor> WithIncludes() =>
            _db.Doctors
               .Include(d => d.DoctorSpecializations)
               .ThenInclude(ds => ds.Specialization);

        public async Task<IEnumerable<Doctor>> GetAllAsync() =>
            await WithIncludes().ToListAsync();

        public async Task<Doctor?> GetByIdAsync(int id) =>
            await WithIncludes().FirstOrDefaultAsync(d => d.Id == id);

        public async Task<Doctor?> GetByUserIdAsync(int userId) =>
            await WithIncludes().FirstOrDefaultAsync(d => d.UserId == userId);

        public async Task<IEnumerable<Doctor>> GetBySpecializationAsync(string specialization) =>
            await WithIncludes()
                .Where(d => d.DoctorSpecializations
                    .Any(ds => ds.Specialization.Name.Contains(specialization)))
                .ToListAsync();

        public async Task<Doctor> CreateAsync(Doctor doctor)
        {
            _db.Doctors.Add(doctor);
            await _db.SaveChangesAsync();
            return doctor;
        }

        public async Task<Doctor?> UpdateAsync(int id, Doctor updated)
        {
            var d = await _db.Doctors.FindAsync(id);
            if (d == null) return null;
            d.FullName        = updated.FullName;
            d.Email           = updated.Email;
            d.Phone           = updated.Phone;
            d.ExperienceYears = updated.ExperienceYears;
            d.UserId          = updated.UserId;
            await _db.SaveChangesAsync();
            return d;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var d = await _db.Doctors.FindAsync(id);
            if (d == null) return false;
            _db.Doctors.Remove(d);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
