using BookStore.Application.DTOs;
using BookStore.Web.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BookStore.Web.Services
{
    public class BookApiService : IBookApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JsonSerializerOptions _jsonOptions;

        public BookApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync("api/v1/books");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<BookDto>>(content, _jsonOptions)
                   ?? new List<BookDto>();
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync($"api/v1/books/{id}");
            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BookDto>(content, _jsonOptions);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId)
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync($"api/v1/books/category/{categoryId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<BookDto>>(content, _jsonOptions)
                   ?? new List<BookDto>();
        }

        public async Task<IEnumerable<BookDto>> SearchBooksAsync(string keyword)
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync($"api/v1/books/search?keyword={keyword}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<BookDto>>(content, _jsonOptions)
                   ?? new List<BookDto>();
        }

        public async Task<BookDto> CreateBookAsync(BookCreateDto dto)
        {
            AddAuthHeader();
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/v1/books", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BookDto>(result, _jsonOptions)!;
        }

        public async Task<BookDto> UpdateBookAsync(BookUpdateDto dto)
        {
            AddAuthHeader();
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/v1/books/{dto.BookId}", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BookDto>(result, _jsonOptions)!;
        }

        public async Task DeleteBookAsync(int id)
        {
            AddAuthHeader();
            var response = await _httpClient.DeleteAsync($"api/v1/books/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
