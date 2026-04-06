using Microsoft.EntityFrameworkCore;

namespace EfCoreBookDemo.Models
{
    public class BookDBContext : DbContext
    {
        public BookDBContext(DbContextOptions<BookDBContext> options) : base(options)
        {
        }

        public DbSet<BookModel> BookModels { get; set; }
    }
}