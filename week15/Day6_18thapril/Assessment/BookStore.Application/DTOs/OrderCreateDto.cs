namespace BookStore.Application.DTOs
{
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public List<OrderItemCreateDto> Items { get; set; } = new();
    }

    public class OrderItemCreateDto
    {
        public int BookId { get; set; }
        public int Qty { get; set; }
    }
}
