namespace StudentAdminPortal.Services
{
    public interface IAuthService
    {
        bool IsAuthenticated(HttpContext context);
    }
}