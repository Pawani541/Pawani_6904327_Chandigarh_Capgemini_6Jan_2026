namespace BookStore.Application.DTOs
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderItemResponseDto> Items { get; set; } = new();
    }

    public class OrderItemResponseDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
