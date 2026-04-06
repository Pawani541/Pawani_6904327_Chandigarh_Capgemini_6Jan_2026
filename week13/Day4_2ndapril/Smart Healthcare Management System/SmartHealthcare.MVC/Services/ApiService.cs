using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SmartHealthcare.MVC.Services.Interfaces;

namespace SmartHealthcare.MVC.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _ctx;
        private static readonly JsonSerializerOptions _json =
            new() { PropertyNameCaseInsensitive = true };

        public ApiService(HttpClient http, IHttpContextAccessor ctx)
        {
            _http = http;
            _ctx  = ctx;
        }

        private void AttachToken()
        {
            var token = _ctx.HttpContext?.Session.GetString("JwtToken");
            if (!string.IsNullOrEmpty(token))
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
        }

        public void SetToken(string token) =>
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            AttachToken();
            var res = await _http.GetAsync(endpoint);
            if (!res.IsSuccessStatusCode) return default;
            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, _json);
        }

        public async Task<T?> PostAsync<T>(string endpoint, object data)
        {
            AttachToken();
            var content = new StringContent(JsonSerializer.Serialize(data),
                Encoding.UTF8, "application/json");
            var res = await _http.PostAsync(endpoint, content);
            if (!res.IsSuccessStatusCode) return default;
            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, _json);
        }

        public async Task<T?> PutAsync<T>(string endpoint, object data)
        {
            AttachToken();
            var content = new StringContent(JsonSerializer.Serialize(data),
                Encoding.UTF8, "application/json");
            var res = await _http.PutAsync(endpoint, content);
            if (!res.IsSuccessStatusCode) return default;
            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, _json);
        }

        public async Task<T?> PatchAsync<T>(string endpoint, object data)
        {
            AttachToken();
            var content = new StringContent(JsonSerializer.Serialize(data),
                Encoding.UTF8, "application/json");
            var res = await _http.PatchAsync(endpoint, content);
            if (!res.IsSuccessStatusCode) return default;
            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, _json);
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            AttachToken();
            var res = await _http.DeleteAsync(endpoint);
            return res.IsSuccessStatusCode;
        }
    }
}
