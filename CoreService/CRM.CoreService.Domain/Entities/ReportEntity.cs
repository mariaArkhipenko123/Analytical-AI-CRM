using CRM.CoreService.Domain.Enums;

namespace CRM.CoreService.Domain.Entities
{
    public class ReportEntity
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public ReportStatus Status { get; set; }
        public Guid UserId { get; set; }
        public Guid FileId { get; set; }
        public UserEntity User { get; set; }
    }
}
