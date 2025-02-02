using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.FileService.Application.Interfaces.Infrastructure
{
    public interface IRedisMessageBroker
    {
        Task PublishAsync<T>(string channel, T message);
        Task SubscribeAsync<T>(string channel, Action<T> handler);
    }
}
