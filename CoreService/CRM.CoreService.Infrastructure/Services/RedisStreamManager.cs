using CRM.CoreService.Application.Interfaces.Infrastructure;
using CRM.CoreService.Domain.Enums;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Collections.Concurrent;
using CRM.CoreService.Infrastructure.MessageBroker;

namespace CRM.CoreService.Infrastructure.Services
{
	public class RedisStreamManager : IStreamManager
	{
		private readonly IRedisMessageBroker _messageBroker;
		private readonly IConfiguration _configuration;
		private readonly string _redisGroup;
		private readonly string _redisConsumerName;
		private readonly string _redisTarget;
		private readonly ConcurrentDictionary<string, RedisStreamListener> _listeners = new();

		public RedisStreamManager(IConnectionMultiplexer connectionMultiplexer, IConfiguration configuration)
		{
			_messageBroker = new RedisMessageBroker(connectionMultiplexer);
			_configuration = configuration;

			_redisGroup = _configuration["RedisStream:Group"] ?? throw new Exception($"Value not found in configuration: RedisStream:Group");
			_redisConsumerName = _configuration["RedisStream:ConsumerName"] ?? throw new Exception($"Value not found in configuration: RedisStream:ConsumerName");
			_redisTarget = _configuration["RedisStream:ThisTarget"] ?? throw new Exception($"Value not found in configuration: RedisStream:ThisTarget");
		}
		public void AddListener(string stream)
		{
			if (!_listeners.ContainsKey(stream))
			{
				var listener = new RedisStreamListener(
					_messageBroker,
					stream,
					_redisGroup,
					_redisConsumerName,
					_redisTarget);
				_listeners.TryAdd(stream, listener);
			}
		}

		public void RemoveListener(string stream)
		{
			if (_listeners.TryRemove(stream, out RedisStreamListener? listener))
			{
				listener.Stop();
			}
		}

		public void StartListener(string stream)
		{
			if (_listeners.TryGetValue(stream, out RedisStreamListener? listener))
				listener.Start();
		}

		public void StopListener(string stream)
		{
			if (_listeners.TryGetValue(stream, out RedisStreamListener? listener))
				listener.Stop();
		}

		public void Subscribe<T>(string stream, string task, Action<T> action)
		{
			if (!_listeners.ContainsKey(stream))
				throw new InvalidOperationException($"No listener exists for stream: {stream}");
			_listeners[stream].AddTask(task, action);
		}

		public void Unsubscribe<T>(string stream, string task, Action<T> action)
		{
			if (!_listeners.ContainsKey(stream))
				throw new InvalidOperationException($"No listener exists for stream: {stream}");
			_listeners[stream].RemoveTask(task, action);
		}

		public async Task SendAsync<T>(string Stream, string target, string task, T Message) =>
			await _messageBroker.WriteMessageToStreamAsync(
				Stream,
				new() {
					{ RedisMetaTag.TargetService, target },
					{ RedisMetaTag.Task, task }
				},
				Message);
	}
}
