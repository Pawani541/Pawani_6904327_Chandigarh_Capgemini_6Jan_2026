using SmartHealthcare.Core.Models;

namespace SmartHealthcare.API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
    }
}
