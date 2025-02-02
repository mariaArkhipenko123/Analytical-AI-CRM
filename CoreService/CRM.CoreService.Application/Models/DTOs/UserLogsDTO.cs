using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Application.Models.DTOs
{
    public class UserLogsDTO
    {
        [BsonId]
        public ObjectId Id { get; set; } = new ObjectId();

        [BsonRepresentation(BsonType.String)]
        public Guid? UserId { get; set; }
        public string Action { get; set; }
        public string IP { get; set; }
        public string Time { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified).ToString("O");
    }
}
