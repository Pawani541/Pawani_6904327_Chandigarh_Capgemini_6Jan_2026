using ECommerceOrderManagement.API.Models;

namespace ECommerceOrderManagement.API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
