using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Events/Register");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();