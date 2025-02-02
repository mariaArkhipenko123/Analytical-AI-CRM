using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.AIService.Application.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration, TimeSpan absoluteExpiration);
        Task SetAsync<T>(string key, T value, Action<DistributedCacheEntryOptions> options);
    }
}
