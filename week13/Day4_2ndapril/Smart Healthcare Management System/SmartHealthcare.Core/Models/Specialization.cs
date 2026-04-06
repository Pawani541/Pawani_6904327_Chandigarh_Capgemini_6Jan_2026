using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.Core.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } = "";
        public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();
    }
}
