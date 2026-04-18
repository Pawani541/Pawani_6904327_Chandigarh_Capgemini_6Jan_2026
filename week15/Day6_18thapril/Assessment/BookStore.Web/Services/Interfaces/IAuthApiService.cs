using BookStore.Application.DTOs;

namespace BookStore.Web.Services.Interfaces
{
    public interface IAuthApiService
    {
        Task<AuthResponseDto?> LoginAsync(UserLoginDto dto);
        Task<bool> RegisterAsync(UserRegisterDto dto);
    }
}
