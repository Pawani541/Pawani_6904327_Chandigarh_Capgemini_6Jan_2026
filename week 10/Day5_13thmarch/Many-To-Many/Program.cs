using Microsoft.EntityFrameworkCore;
using Model_level_Validation.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

// Default route (Direct open Customers page)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customers}/{action=Index}/{id?}");

// 🔥 AUTO DATA INSERT
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Insert only if empty
    if (!context.Customers.Any() && !context.Products.Any())
    {
        context.Customers.AddRange(
            new Customer { Name = "Rahul" },
            new Customer { Name = "Priya" }
        );

        context.Products.AddRange(
            new Product { ProductName = "Laptop", Price = 50000 },
            new Product { ProductName = "Phone", Price = 20000 }
        );

        context.SaveChanges();
    }
}

app.Run();