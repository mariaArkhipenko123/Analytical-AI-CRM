using CRM.ReportService.Application.Models.RedisStreamDTOs;

namespace CRM.ReportService.Application.Interfaces.Infrastructure
{
	public interface IRedisMessageBroker
	{
		Task AcknowledgeMessagesAsync(string streamName, string consumerGroup, IEnumerable<string> entries);
		Task<List<RedisMessage>> ReadMessagesFromStreamAsync(string streamName, string consumerGroup, string consumerName, string targetService);
		Task TryCreateConsumerGroupAsync(string streamName, string consumerGroup);
		Task WriteMessageToStreamAsync<T>(string streamName, Dictionary<string, string> meta, T value);
	}
}