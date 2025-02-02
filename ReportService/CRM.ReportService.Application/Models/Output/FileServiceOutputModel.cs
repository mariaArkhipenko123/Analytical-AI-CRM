namespace CRM.ReportService.Application.Models.Output
{
    public class FileServiceOutputModel
    {
        public Guid UserId { get; set; }       
        public string FileType { get; set; }  
        public string FileName { get; set; }  
        public string FileContent { get; set; } 
        public Guid FileId { get; internal set; }
        public DateTime CreatedAt { get; set; } 
    }
}
