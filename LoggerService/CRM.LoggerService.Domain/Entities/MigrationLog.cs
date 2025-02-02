using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.LoggerService.Domain.Migrations
{
    public class MigrationLog
    {
        public ObjectId Id { get; set; }
        public string Version { get; set; } = string.Empty; 
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
        public bool RolledBack { get; set; } = false;
    }
}
