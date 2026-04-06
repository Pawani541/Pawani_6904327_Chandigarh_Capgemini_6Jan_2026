using Microsoft.Extensions.Caching.Distributed;
using ECommerceOrderManagement.API.Services.Interfaces;

namespace ECommerceOrderManagement.API.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache) { _cache = cache; }

        public async Task<string?> GetAsync(string key) =>
            await _cache.GetStringAsync(key);

        public async Task SetAsync(string key, string value, TimeSpan? expiry = null) =>
            await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(5)
            });

        public async Task RemoveAsync(string key) =>
            await _cache.RemoveAsync(key);
    }
}
