using SmartHealthcare.Core.DTOs;

namespace SmartHealthcare.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
        Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto?> RefreshAsync(string refreshToken);
    }
}
