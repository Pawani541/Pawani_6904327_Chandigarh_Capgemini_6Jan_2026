namespace BookStore.Application.DTOs
{
    public class WishlistDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
