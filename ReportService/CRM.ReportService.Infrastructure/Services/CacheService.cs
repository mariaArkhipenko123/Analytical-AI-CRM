using CRM.ReportService.Application.Interfaces.Infrastructure;
using CRM.ReportService.Infrastructure.Redis;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace CRM.ReportService.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            return await _distributedCache.GetAsync<T>(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration, TimeSpan absoluteExpiration)
        {
            await _distributedCache.SetAsync(key, value, slidingExpiration, absoluteExpiration);
        }
        public async Task SetAsync<T>(string key, T value, Action<DistributedCacheEntryOptions> options)
        {
            var jsonString = JsonSerializer.Serialize(value);
            var data = Encoding.UTF8.GetBytes(jsonString);

            var cacheEntryOptions = new DistributedCacheEntryOptions();
            options(cacheEntryOptions);

            await _distributedCache.SetAsync(key, data, cacheEntryOptions);
        }
        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

    }
}
