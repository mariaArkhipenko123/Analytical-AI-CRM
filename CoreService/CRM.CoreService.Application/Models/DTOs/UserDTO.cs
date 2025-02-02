namespace CRM.CoreService.Application.Models.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ArchivedAt { get; set; }
        public IEnumerable<ReportDTO> Reports { get; set; }
    }
}
