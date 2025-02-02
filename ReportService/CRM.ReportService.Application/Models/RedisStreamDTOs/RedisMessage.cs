namespace CRM.ReportService.Application.Models.RedisStreamDTOs
{
	public class RedisMessage
	{
		public required string Id { get; set; }
		public required string Task { get; set; }
		public required string Data { get; set; }
	}
}
