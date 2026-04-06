using Middleware_for_Request_Tracking.Services;
using Middleware_for_Request_Tracking.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register logging service
builder.Services.AddSingleton<IRequestLogService, RequestLogService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Custom middleware
app.UseMiddleware<RequestTrackingMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}");

app.Run();