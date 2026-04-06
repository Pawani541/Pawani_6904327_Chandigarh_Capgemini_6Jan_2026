using Microsoft.EntityFrameworkCore;
using LearningAPI.Data;

namespace LearningAPI.Tests
{
    public static class DbContextHelper
    {
        public static AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }
    }
}
