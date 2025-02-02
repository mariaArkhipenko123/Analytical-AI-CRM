using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace CRM.CoreService.Infrastructure.Extensions.Redis
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, TimeSpan slidingExpiration, TimeSpan absoluteExpiration)
        {
            var options = new DistributedCacheEntryOptions()
           .SetSlidingExpiration(slidingExpiration)
           .SetAbsoluteExpiration(absoluteExpiration);

            await cache.SetAsync(
                key,
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value)),
                options);
        }

        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, Action<DistributedCacheEntryOptions> options)
        {
            var newOptions = new DistributedCacheEntryOptions();
            options.Invoke(newOptions);

            await cache.SetAsync(
                key,
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value)),
                newOptions);
        }

        public static async Task<T?> GetAsync<T>(this IDistributedCache cache, string key)
        {
            var cachedBytes = await cache.GetAsync(key);

            if (cachedBytes is null)
                return default;

            return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(cachedBytes));
        }
        public static async Task RemoveAsync(this IDistributedCache cache, string key)
        {
            await cache.RemoveAsync(key);
        }
    }
}
