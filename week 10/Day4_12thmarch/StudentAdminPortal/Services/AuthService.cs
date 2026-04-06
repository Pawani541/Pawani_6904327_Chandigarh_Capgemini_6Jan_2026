using Microsoft.AspNetCore.Http;
namespace StudentAdminPortal.Services
{
    public class AuthService : IAuthService
    {
        public bool IsAuthenticated(HttpContext context)
        {
            return context.Session.GetString("User") != null;
        }
    }
}