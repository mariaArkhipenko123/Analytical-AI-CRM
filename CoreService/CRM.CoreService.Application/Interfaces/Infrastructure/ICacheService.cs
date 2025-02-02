using Microsoft.Extensions.Caching.Distributed;

namespace CRM.CoreService.Infrastructure.Services
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync(string key);
        Task SetAsync<T>(string key, T value, Action<DistributedCacheEntryOptions> options);
        Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration, TimeSpan absoluteExpiration);
    }
}