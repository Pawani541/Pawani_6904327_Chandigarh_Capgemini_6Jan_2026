using LearningAPI.Data;
using LearningAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningAPI.Tests
{
    public class CourseTests
    {
        [Fact]
        public async Task GetCourseById_ShouldReturnCourse_WhenExists()
        {
            var context = DbContextHelper.GetInMemoryDbContext();
            context.Courses.Add(new Course { Id = 1, Title = "C# Basics" });
            await context.SaveChangesAsync();

            var result = await context.Courses.FindAsync(1);

            Assert.NotNull(result);
            Assert.Equal("C# Basics", result.Title);
        }

        [Fact]
        public async Task CreateCourse_ShouldAddToDatabase()
        {
            var context = DbContextHelper.GetInMemoryDbContext();
            context.Courses.Add(new Course { Id = 2, Title = "ASP.NET Core" });
            await context.SaveChangesAsync();

            var saved = await context.Courses.FirstOrDefaultAsync(c => c.Title == "ASP.NET Core");
            Assert.NotNull(saved);
        }

        [Fact]
        public async Task CreateCourse_ShouldFail_WhenTitleIsEmpty()
        {
            var context = DbContextHelper.GetInMemoryDbContext();
            context.Courses.Add(new Course { Id = 3, Title = "" });
            await context.SaveChangesAsync();

            var saved = await context.Courses.FindAsync(3);
            Assert.NotNull(saved);
            Assert.Equal("", saved.Title);
        }
    }
}
