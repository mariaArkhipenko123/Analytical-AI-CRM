using CRM.CoreService.Application.Interfaces.Infrastructure;
using CRM.CoreService.Application.Models.RedisStreamDTOs;
using System.Collections.Concurrent;
using System.Text.Json;

namespace CRM.CoreService.Infrastructure.MessageBroker
{
	public class RedisStreamListener
	{
		private readonly IRedisMessageBroker _messageBroker;
		private readonly string _redisStream;
		private readonly string _redisGroup;
		private readonly string _redisConsumerName;
		private readonly string _redisTarget;
		private Task? _listenerTask;
		private readonly ConcurrentDictionary<string, List<RedisAction>> _tasks = new();

		private CancellationTokenSource? _tokenSource;

		private bool _isListening = false;
		public bool isListening { get => _isListening; }

		public int PollRate = 1000;

		public RedisStreamListener(IRedisMessageBroker messageBroker, string redisStream, string redisGroup, string redisConsumerName, string redisTarget)
		{
			_messageBroker = messageBroker;
			_redisStream = redisStream;
			_redisGroup = redisGroup;
			_redisConsumerName = redisConsumerName;
			_redisTarget = redisTarget;
		}
		public void AddTask<T>(string Task, Action<T> action)
		{
			if (_tasks.TryGetValue(Task, out List<RedisAction>? actions))
				actions.Add(new RedisAction(action, typeof(T)));
			else
				_tasks.GetOrAdd(Task, [new RedisAction(action, typeof(T))]);
		}
		public void RemoveTask<T>(string Task, Action<T> action)
		{
			if (_tasks.TryGetValue(Task, out List<RedisAction>? actions))
				actions.Remove(new RedisAction(action, typeof(T)));
		}
		public void Start()
		{
			if (_isListening) return;

			_isListening = true;
			_tokenSource = new CancellationTokenSource();
			_listenerTask = Task.Run(() => Listen(_tokenSource.Token));
		}
		public void Stop()
		{
			_tokenSource?.Cancel();
			_isListening = false;
		}
		private async Task Listen(CancellationToken cancellationToken)
		{
			await _messageBroker.TryCreateConsumerGroupAsync(_redisStream, _redisGroup);
			while (!cancellationToken.IsCancellationRequested)
			{
				var messages = await _messageBroker.ReadMessagesFromStreamAsync(_redisStream, _redisGroup, _redisConsumerName, _redisTarget);
				if (messages is not null && messages.Count > 0)
				{
					foreach (RedisMessage message in messages)
					{
						if (_tasks[message.Task].Count > 0)
						{
							foreach (var redisAction in _tasks[message.Task])
							{
								var data = JsonSerializer.Deserialize(message.Data, redisAction.ExpectedType);
								redisAction.Action?.DynamicInvoke(data);
								await _messageBroker.AcknowledgeMessagesAsync(_redisStream, _redisGroup, [message.Id]);
							}
						}
					}
				}
				Thread.Sleep(PollRate);
			}
		}
	}
}
