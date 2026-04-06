using System.Net;
using System.Text.Json;

namespace LearningAPI.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);

            if (context.Response.StatusCode == 404)
            {
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { error = "Resource not found" });
                await context.Response.WriteAsync(result);
            }
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new { error = "Internal server error", detail = ex.Message });
            await context.Response.WriteAsync(result);
        }
    }
}
