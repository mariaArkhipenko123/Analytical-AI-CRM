using MongoDB.Bson;

namespace CRM.LoggerService.Domain.Entities
{
    public class UserLog
    {
        public ObjectId Id { get; set; }
        public string? UserId { get; set; }
        public string? Action { get; set; }
        public string? IpAddress { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
