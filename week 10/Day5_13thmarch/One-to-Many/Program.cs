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

// Default Route (Direct open Companies/Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Companies}/{action=Index}/{id?}");

// 🔥 AUTO DATA INSERT (No need to type anything in browser)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Companies.Any())
    {
        var company1 = new Company { Name = "TCS" };
        var company2 = new Company { Name = "Infosys" };

        var emp1 = new Employee { Name = "Rahul", Age = 25, Company = company1 };
        var emp2 = new Employee { Name = "Priya", Age = 28, Company = company1 };
        var emp3 = new Employee { Name = "Amit", Age = 30, Company = company2 };

        context.Companies.AddRange(company1, company2);
        context.Employees.AddRange(emp1, emp2, emp3);

        context.SaveChanges();
    }
}

app.Run();