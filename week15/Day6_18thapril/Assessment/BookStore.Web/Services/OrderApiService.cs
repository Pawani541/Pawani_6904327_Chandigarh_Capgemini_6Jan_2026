using BookStore.Application.DTOs;
using BookStore.Web.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BookStore.Web.Services
{
    public class OrderApiService : IOrderApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JsonSerializerOptions _jsonOptions;

        public OrderApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        private void AddAuthHeader()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync("api/v1/orders");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<OrderResponseDto>>(content, _jsonOptions)
                   ?? new List<OrderResponseDto>();
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync($"api/v1/orders/{id}");
            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OrderResponseDto>(content, _jsonOptions);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByUserAsync(int userId)
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync($"api/v1/orders/user/{userId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<OrderResponseDto>>(content, _jsonOptions)
                   ?? new List<OrderResponseDto>();
        }

        public async Task<OrderResponseDto> PlaceOrderAsync(OrderCreateDto dto)
        {
            AddAuthHeader();
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/v1/orders", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OrderResponseDto>(result, _jsonOptions)!;
        }
    }
}
