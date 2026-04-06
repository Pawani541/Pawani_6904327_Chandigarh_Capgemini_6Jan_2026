using ECommerceOrderManagement.API.DTOs;
using ECommerceOrderManagement.API.Models;
using ECommerceOrderManagement.API.Repositories.Interfaces;
using ECommerceOrderManagement.API.Services.Interfaces;

namespace ECommerceOrderManagement.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            if (await _userRepo.GetByEmailAsync(dto.Email) != null) return false;
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };
            await _userRepo.AddAsync(user);
            return true;
        }

        public async Task<TokenResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;
            var access = _tokenService.GenerateAccessToken(user);
            var refresh = _tokenService.GenerateRefreshToken();
            user.RefreshToken = new RefreshToken
            {
                Token = refresh,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };
            await _userRepo.UpdateAsync(user);
            return new TokenResponseDto { AccessToken = access, RefreshToken = refresh };
        }
    }
}
