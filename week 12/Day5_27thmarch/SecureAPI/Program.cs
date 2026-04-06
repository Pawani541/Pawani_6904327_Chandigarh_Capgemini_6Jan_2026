using log4net;
using log4net.Config;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ✅ Swagger with JWT Authorize button
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer <token>'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// ✅ Log4Net setup
var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
XmlConfigurator.Configure(LogManager.GetRepository(assembly), new FileInfo("log4net.config"));

var log = LogManager.GetLogger(typeof(Program));

// ✅ JWT setup (safe)
var keyString = builder.Configuration["Jwt:Key"];

if (string.IsNullOrEmpty(keyString))
{
    throw new Exception("JWT Key missing in appsettings.json");
}

var key = Encoding.UTF8.GetBytes(keyString);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    // ✅ JWT logging
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            log.Warn("Invalid token");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            log.Warn($"Unauthorized access to {context.Request.Path}");
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// ✅ Request Logging Middleware
app.Use(async (context, next) =>
{
    var logger = LogManager.GetLogger("Middleware");

    logger.Info($"{context.Request.Method} {context.Request.Path} called");

    await next();

    if (context.Response.StatusCode == 401)
        logger.Warn($"Unauthorized access to {context.Request.Path}");

    if (context.Response.StatusCode == 403)
        logger.Warn($"Forbidden access to {context.Request.Path}");
});

// ✅ Swagger
app.UseSwagger();
app.UseSwaggerUI();

// ✅ Auth middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();