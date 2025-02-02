using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.FileService.Domain.Entities
{
    public class FileEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Path { get; set; }
        public string Extension { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
    }
}
