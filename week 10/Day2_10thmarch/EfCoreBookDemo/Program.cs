using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var cs1 = builder.Configuration.GetConnectionString("cs1");

builder.Services.AddDbContext<BookDBContext>(options =>
options.UseSqlServer(cs1));

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();