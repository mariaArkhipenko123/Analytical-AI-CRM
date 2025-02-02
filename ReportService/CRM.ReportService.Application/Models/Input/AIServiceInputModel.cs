namespace CRM.ReportService.Application.Models.Input
{
    public class AIServiceInputModel
    {
        public Guid UserId { get; set; }
        public string FileType { get; set; } 
        public string Content { get; set; } 
    }
}
