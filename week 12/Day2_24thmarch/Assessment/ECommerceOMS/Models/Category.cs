using System.ComponentModel.DataAnnotations;

namespace ECommerceOMS.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}