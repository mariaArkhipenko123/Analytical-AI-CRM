namespace CRM.CoreService.Application.Models.Inputs
{
    public class CreateReportInput
    {
        public string Type { get; set; }  
        public string Status { get; set; }  
        public Guid UserId { get; set; }
        public Guid FileId { get; set; }
    }
}
