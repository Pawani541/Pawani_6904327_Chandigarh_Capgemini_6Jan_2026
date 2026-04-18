namespace BookStore.Domain.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
