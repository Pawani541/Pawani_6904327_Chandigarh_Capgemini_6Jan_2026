using System.ComponentModel.DataAnnotations;

namespace ECommerceOMS.Models
{
    public class ShippingDetail
    {
        [Key]
        public int ShippingId { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [StringLength(50)]
        public string? Status { get; set; } = "Pending";

        [Display(Name = "Shipped Date")]
        public DateTime? ShippedDate { get; set; }     // ← already nullable

        [StringLength(100)]
        [Display(Name = "Tracking Number")]
        public string? TrackingNumber { get; set; }    // ← fixed

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}