namespace BookStore.Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
    }
}
