using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.LoggerService.Domain.Entities
{
    public class GraphQLLog
    {
        public ObjectId Id { get; set; }
        public required string UserId { get; set; }
        public required string Query { get; set; }
        public object Variables { get; set; }
        public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
        public required double ExecutionTimeMs { get; set; }
        public string? RequestOrigin { get; set; } //Add this field for testing migration
    }
}
