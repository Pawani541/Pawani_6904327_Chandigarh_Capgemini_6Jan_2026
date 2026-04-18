using BookStore.Shared;
using System.Net;
using System.Text.Json;

namespace BookStore.API.Middleware
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

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                Timestamp = DateTime.UtcNow
            };

            switch (ex)
            {
                case KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Status = (int)HttpStatusCode.NotFound;
                    response.Message = ex.Message;
                    break;

                case InvalidOperationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = (int)HttpStatusCode.BadRequest;
                    response.Message = ex.Message;
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Status = (int)HttpStatusCode.Unauthorized;
                    response.Message = "Unauthorized access.";
                    break;

                case ArgumentException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = (int)HttpStatusCode.BadRequest;
                    response.Message = ex.Message;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Status = (int)HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong. Please try again later.";
                    response.Details = ex.Message;
                    break;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}
