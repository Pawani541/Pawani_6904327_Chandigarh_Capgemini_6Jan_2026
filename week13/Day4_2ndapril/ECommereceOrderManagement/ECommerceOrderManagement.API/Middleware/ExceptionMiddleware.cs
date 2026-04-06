using System.Text.Json;

namespace ECommerceOrderManagement.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                ctx.Response.ContentType = "application/json";
                ctx.Response.StatusCode = ex switch
                {
                    ArgumentException => 400,
                    UnauthorizedAccessException => 401,
                    KeyNotFoundException => 404,
                    _ => 500
                };
                await ctx.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    ctx.Response.StatusCode,
                    ex.Message
                }));
            }
        }
    }
}
