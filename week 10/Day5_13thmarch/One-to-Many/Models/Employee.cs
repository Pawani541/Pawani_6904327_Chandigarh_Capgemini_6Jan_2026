using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model_level_Validation.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee Name is required")]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
        public int Age { get; set; }

        // Foreign Key
        [Required(ErrorMessage = "Company is required")]
        public int CompanyId { get; set; }

        // Navigation Property 
        public Company? Company { get; set; }
    }
}