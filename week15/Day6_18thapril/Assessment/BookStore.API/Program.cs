using Asp.Versioning;
using BookStore.API.Extensions;
using BookStore.API.Middleware;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/bookstore-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapperProfiles();
builder.Services.AddFluentValidators();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMVC", policy =>
        policy.WithOrigins("https://localhost:7001", "http://localhost:5001")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "BookStore Pro API";
        options.Theme = ScalarTheme.Purple;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowMVC");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
