namespace Model_level_Validation.Models
{
    public class Order
    {
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}