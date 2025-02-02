using MongoDB.Bson;

namespace CRM.LoggerService.Domain.Entities
{
    public class FileLog
    {
        public ObjectId Id { get; set; }
        public required string FileType { get; set; }
        public required ObjectId ReportId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
