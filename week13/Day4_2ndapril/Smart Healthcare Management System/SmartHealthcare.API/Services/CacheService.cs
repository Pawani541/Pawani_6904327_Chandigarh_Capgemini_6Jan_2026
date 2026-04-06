using Microsoft.Extensions.Caching.Memory;
using SmartHealthcare.API.Services.Interfaces;

namespace SmartHealthcare.API.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        public CacheService(IMemoryCache cache) { _cache = cache; }

        public T? Get<T>(string key) => _cache.TryGetValue(key, out T? val) ? val : default;

        public void Set<T>(string key, T value, TimeSpan? expiry = null) =>
            _cache.Set(key, value, expiry ?? TimeSpan.FromMinutes(10));

        public void Remove(string key) => _cache.Remove(key);
    }
}
