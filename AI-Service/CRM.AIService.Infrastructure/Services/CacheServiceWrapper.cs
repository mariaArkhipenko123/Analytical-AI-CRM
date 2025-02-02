using CRM.AIService.Application.Interfaces;
using CRM.AIService.Infrastructure.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.AIService.Infrastructure.Services
{
    public class CacheServiceWrapper : ICacheService
    {
        private readonly IDistributedCache _cache;
        public CacheServiceWrapper(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<T?> GetAsync<T>(string key)
        {
            return await _cache.GetAsync<T>(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration, TimeSpan absoluteExpiration)
        {
            await _cache.SetAsync<T>(key, value, slidingExpiration, absoluteExpiration); 
        }

        public async Task SetAsync<T>(string key, T value, Action<DistributedCacheEntryOptions> options)
        {
            await _cache.SetAsync<T>(key, value, options);
        }
    }
}
