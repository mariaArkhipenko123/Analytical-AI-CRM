using CRM.CoreService.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.CoreService.Domain.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {
        public UserStatus Status { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "timestamp")]
        public DateTime? UpdatedAt { get; set; } 

        [Column(TypeName = "timestamp")]
        public DateTime? ArchivedAt { get; set; }
        public string? Issuer { get; set; }     //optional field for external auth providers
        public bool IsTemporaryPassword { get; set; }
        public ICollection<ReportEntity> Reports { get; set; }
    }
}
