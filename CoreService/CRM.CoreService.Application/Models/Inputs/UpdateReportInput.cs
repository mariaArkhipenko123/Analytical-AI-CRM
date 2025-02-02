namespace CRM.CoreService.Application.Models.Inputs
{
    public class UpdateReportInput
    {
        public Guid Id { get; set; }       
        public string? Type { get; set; }   
        public string? Status { get; set; }
    }
}
