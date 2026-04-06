using Microsoft.EntityFrameworkCore;
using SmartHealthcare.API.Data;
using SmartHealthcare.API.Repositories.Interfaces;
using SmartHealthcare.Core.Models;

namespace SmartHealthcare.API.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _db;
        public PatientRepository(AppDbContext db) { _db = db; }

        public async Task<IEnumerable<Patient>> GetAllAsync() =>
            await _db.Patients.Include(p => p.User).ToListAsync();

        public async Task<Patient?> GetByIdAsync(int id) =>
            await _db.Patients.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Patient?> GetByUserIdAsync(int userId) =>
            await _db.Patients.FirstOrDefaultAsync(p => p.UserId == userId);

        public async Task<Patient> CreateAsync(Patient patient)
        {
            _db.Patients.Add(patient);
            await _db.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient?> UpdateAsync(int id, Patient updated)
        {
            var p = await _db.Patients.FindAsync(id);
            if (p == null) return null;
            p.FullName = updated.FullName; p.Phone = updated.Phone;
            p.DateOfBirth = updated.DateOfBirth; p.Gender = updated.Gender;
            p.Address = updated.Address;
            await _db.SaveChangesAsync();
            return p;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _db.Patients.FindAsync(id);
            if (p == null) return false;
            _db.Patients.Remove(p);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
