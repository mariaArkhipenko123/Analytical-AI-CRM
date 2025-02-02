namespace CRM.ReportService.Domain.Entities
{
    public class Report
    {
        public Guid Id { get; set; } 
        public Guid UserId { get; set; } 
        public string Type { get; set; } 
        public string Status { get; set; } 
        public Guid FileId { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
