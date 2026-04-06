using Microsoft.EntityFrameworkCore;
using EventRegistrationRP.Models;

namespace EventRegistrationRP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<EventRegistration> Registrations { get; set; }
    }
}