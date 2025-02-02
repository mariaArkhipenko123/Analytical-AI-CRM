namespace CRM.CoreService.Application.Models.DTOs
{
    public class ReportDTO
    {
        public Guid Id { get; set; } 
        public string Type { get; set; } 
        public string Status { get; set; }
        public Guid FileId { get; set; } 
    }
}
