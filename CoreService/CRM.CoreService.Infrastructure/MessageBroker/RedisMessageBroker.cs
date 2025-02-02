using CRM.CoreService.Application.Interfaces.Infrastructure;
using CRM.CoreService.Application.Models.RedisStreamDTOs;
using CRM.CoreService.Domain.Enums;
using StackExchange.Redis;
using System.Text.Json;

namespace CRM.CoreService.Infrastructure.MessageBroker
{
	public class RedisMessageBroker : IRedisMessageBroker
	{
		private readonly IDatabase _db;
		public RedisMessageBroker(IConnectionMultiplexer connectionMultiplexer)
		{
			_db = connectionMultiplexer.GetDatabase();
		}
		public async Task WriteMessageToStreamAsync<T>(string streamName, Dictionary<string, string> meta, T value)
		{
			var message = meta
				.Select(v => new NameValueEntry(v.Key, v.Value))
				.ToList();
			message.Add(
				new NameValueEntry(RedisMetaTag.Data, JsonSerializer.Serialize(value))
				);
			await _db.StreamAddAsync(streamName, message.ToArray());
		}
		public Task<List<RedisMessage>> ReadMessagesFromStreamAsync(
			string streamName,
			string consumerGroup,
			string consumerName,
			string targetService) =>
			ReadMessagesFromStreamAsync(streamName, consumerGroup, consumerName, targetService, ">", 10);

		public async Task<List<RedisMessage>> ReadMessagesFromStreamAsync(
			string streamName,
			string consumerGroup,
			string consumerName,
			string targetService,
			string lastId = "0",
			int count = 10
		)
		{
			try
			{
				var entries = await _db.StreamReadGroupAsync(streamName, consumerGroup, consumerName, lastId, count);
				return entries
					.Where(entry => entry[RedisMetaTag.TargetService] == targetService)
					.Select(entry => new RedisMessage()
					{
						Id = entry.Id.ToString(),
						Task = entry[RedisMetaTag.Task].ToString(),
						Data = entry[RedisMetaTag.Data].ToString()
					})
					.ToList();
			}
			catch (RedisException ex)
			{
				return new();
			}
		}
		public async Task AcknowledgeMessagesAsync(string streamName, string consumerGroup, IEnumerable<string> entries)
		{
			if (entries == null || entries.Count() == 0)
				return;
			await _db.StreamAcknowledgeAsync(streamName, consumerGroup, entries.Select(e => new RedisValue(e)).ToArray());
		}
		public async Task TryCreateConsumerGroupAsync(string streamName, string consumerGroup)
		{
			try
			{
				await _db.StreamCreateConsumerGroupAsync(streamName, consumerGroup, "0-0", true);
			}
			catch (RedisServerException ex) when (ex.Message.Contains("BUSYGROUP"))
			{
				//Consumer Group already exists, ingore this exception.
			}
		}
	}
}

