using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.API.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        public string? Description { get; set; }

        // Navigation
        public ICollection<Doctor>? Doctors { get; set; }
    }
}
