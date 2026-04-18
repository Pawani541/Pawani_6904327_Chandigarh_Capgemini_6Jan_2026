namespace BookStore.Domain.Entities
{
    public class Wishlist
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
