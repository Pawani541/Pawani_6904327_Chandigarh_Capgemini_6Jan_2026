using log4net;
using log4net.Config;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ FIX: Null-safe assembly (no warning)
var assembly = Assembly.GetEntryAssembly();

if (assembly == null)
{
    assembly = Assembly.GetExecutingAssembly();
}

// ✅ Load log4net config safely
var configFile = new FileInfo("log4net.config");

if (!configFile.Exists)
{
    throw new FileNotFoundException("log4net.config file not found");
}

var logRepository = LogManager.GetRepository(assembly);
XmlConfigurator.Configure(logRepository, configFile);

var app = builder.Build();

// ✅ Logger for global errors
var log = LogManager.GetLogger(typeof(Program));

// ✅ Global Exception Middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        log.Error("Unhandled Exception", ex);
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Internal Server Error");
    }
});

// ✅ Swagger (no error)
app.UseSwagger();
app.UseSwaggerUI();

// ✅ Middleware pipeline
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
log.Info("Application started successfully");

app.Run();