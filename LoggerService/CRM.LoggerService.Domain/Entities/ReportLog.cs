using MongoDB.Bson;

namespace CRM.LoggerService.Domain.Entities
{
    public class ReportLog
    {
        public ObjectId Id { get; set; }
        public required string ReportType { get; set; }
        public required string Status { get; set; }
        public required string RequestedBy { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; } 
    }
}
