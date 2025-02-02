namespace CRM.CoreService.Application.Models.Inputs
{
    public class UpdateUserInput
    {
        public Guid Id { get; set; } // ID of the user to update
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Status { get; set; }
    }
}
