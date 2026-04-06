using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}