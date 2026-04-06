using CollegeDBApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeDBApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CollegeDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeDB")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Authors}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }


}
