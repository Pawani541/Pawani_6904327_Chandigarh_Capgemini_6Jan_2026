using SmartHealthcare.Core.Models;

namespace SmartHealthcare.API.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int id);
        Task<Doctor?> GetByUserIdAsync(int userId);
        Task<IEnumerable<Doctor>> GetBySpecializationAsync(string specialization);
        Task<Doctor> CreateAsync(Doctor doctor);
        Task<Doctor?> UpdateAsync(int id, Doctor updated);
        Task<bool> DeleteAsync(int id);
    }
}
