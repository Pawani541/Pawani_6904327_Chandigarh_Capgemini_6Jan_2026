using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
    }
}
