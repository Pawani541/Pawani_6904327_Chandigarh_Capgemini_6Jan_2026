using System.ComponentModel.DataAnnotations;

namespace Model_level_Validation.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Range(1, 100000)]
        public int Price { get; set; }

        public List<Order>? Orders { get; set; }
    }
}