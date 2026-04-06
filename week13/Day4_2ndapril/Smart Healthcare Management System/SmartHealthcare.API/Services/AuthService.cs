using SmartHealthcare.API.Repositories.Interfaces;
using SmartHealthcare.API.Services.Interfaces;
using SmartHealthcare.Core.DTOs;
using SmartHealthcare.Core.Models;

namespace SmartHealthcare.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly ITokenService _tokens;
        private readonly IPatientRepository _patients;
        private readonly IDoctorRepository _doctors;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository users, ITokenService tokens,
            IPatientRepository patients, IDoctorRepository doctors,
            ILogger<AuthService> logger)
        { _users = users; _tokens = tokens; _patients = patients; _doctors = doctors; _logger = logger; }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            _logger.LogInformation("Login attempt for {Email}", dto.Email);
            var user = await _users.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                _logger.LogWarning("Failed login for {Email}", dto.Email);
                return null;
            }
            return await BuildResponse(user);
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
        {
            var existing = await _users.GetByEmailAsync(dto.Email);
            if (existing != null) return null;
            var user = new User
            {
                Username = dto.Username, Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };
            await _users.CreateAsync(user);
            return await BuildResponse(user);
        }

        public async Task<AuthResponseDto?> RefreshAsync(string refreshToken)
        {
            var stored = await _users.GetRefreshTokenAsync(refreshToken);
            if (stored == null || stored.ExpiresAt < DateTime.UtcNow) return null;
            stored.IsRevoked = true;
            var user = await _users.GetByIdAsync(stored.UserId);
            if (user == null) return null;
            return await BuildResponse(user);
        }

        private async Task<AuthResponseDto> BuildResponse(User user)
        {
            var jwt = _tokens.GenerateJwtToken(user);
            var refresh = _tokens.GenerateRefreshToken();
            await _users.SaveRefreshTokenAsync(new RefreshToken
            {
                UserId = user.Id, Token = refresh,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            int profileId = 0;
            if (user.Role == "Patient")
            {
                var patient = await _patients.GetByUserIdAsync(user.Id);
                profileId = patient?.Id ?? 0;
            }
            else if (user.Role == "Doctor")
            {
                var doctor = await _doctors.GetByUserIdAsync(user.Id);
                profileId = doctor?.Id ?? 0;
            }

            return new AuthResponseDto
            {
                Token = jwt, RefreshToken = refresh,
                Role = user.Role, Username = user.Username,
                UserId = user.Id, ProfileId = profileId
            };
        }
    }
}
