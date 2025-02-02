using StackExchange.Redis;
using CRM.FileService.Application.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace CRM.FileService.Infrastructure.Services
{
    public class RedisMessageBroker : IRedisMessageBroker
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisMessageBroker(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task PublishAsync<T>(string channel, T message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var serializedMessage = JsonSerializer.Serialize(message);

            var subscriber = _redis.GetSubscriber();
            await subscriber.PublishAsync(channel, serializedMessage);
        }

        public async Task SubscribeAsync<T>(string channel, Action<T> handler)
        {
            var subscriber = _redis.GetSubscriber();
            await subscriber.SubscribeAsync(channel, (redisChannel, value) =>
            {
                var message = JsonSerializer.Deserialize<T>(value);

                if (message != null)
                {
                    handler(message);
                }
            });
        }
    }
}
