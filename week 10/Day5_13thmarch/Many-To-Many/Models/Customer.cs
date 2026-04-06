using System.ComponentModel.DataAnnotations;

namespace Model_level_Validation.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public List<Order>? Orders { get; set; }
    }
}