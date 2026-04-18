using AutoMapper;
using BookStore.Application.Interfaces;
using BookStore.Application.MappingProfiles;
using BookStore.Application.Services;
using BookStore.Application.Validators;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStore.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDatabaseServices(
            this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IReportService, ReportService>();
            return services;
        }

        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<BookProfile>();
                    cfg.AddProfile<OrderProfile>();
                });
                return config.CreateMapper();
            });
            services.AddScoped<IMapper>(provider =>
                provider.GetRequiredService<IMapper>());
            return services;
        }

        public static IServiceCollection AddFluentValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<BookCreateValidator>();
            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services, IConfiguration config)
        {
            var jwtSettings = config.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

            services.AddAuthentication(options =>
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
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            return services;
        }
    }
}
