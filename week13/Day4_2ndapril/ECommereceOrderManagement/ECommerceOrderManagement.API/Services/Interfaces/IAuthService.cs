using ECommerceOrderManagement.API.DTOs;

namespace ECommerceOrderManagement.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);
        Task<TokenResponseDto?> LoginAsync(LoginDto dto);
    }
}
