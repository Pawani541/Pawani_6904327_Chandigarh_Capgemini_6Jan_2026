using StudentAdminPortal.Services;

namespace StudentAdminPortal.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAuthService authService)
        {
            if (context.Request.Path.StartsWithSegments("/Admin"))
            {
                if (!authService.IsAuthenticated(context))
                {
                    context.Response.Redirect("/Account/Login");
                    return;
                }
            }

            await _next(context);
        }
    }
} 