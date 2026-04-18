namespace BookStore.Application.DTOs
{
    public class BookUpdateDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
    }
}
