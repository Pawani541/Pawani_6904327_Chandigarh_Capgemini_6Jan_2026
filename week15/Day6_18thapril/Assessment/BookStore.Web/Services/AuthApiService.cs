using BookStore.Application.DTOs;
using BookStore.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace BookStore.Web.Services
{
    public class AuthApiService : IAuthApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<AuthResponseDto?> LoginAsync(UserLoginDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/v1/auth/login", content);
            if (!response.IsSuccessStatusCode) return null;
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AuthResponseDto>(result, _jsonOptions);
        }

        public async Task<bool> RegisterAsync(UserRegisterDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/v1/auth/register", content);
            return response.IsSuccessStatusCode;
        }
    }
}
