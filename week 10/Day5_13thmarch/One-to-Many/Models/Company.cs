using System.ComponentModel.DataAnnotations;

namespace Model_level_Validation.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Employee> Employees { get; set; }
    }
}