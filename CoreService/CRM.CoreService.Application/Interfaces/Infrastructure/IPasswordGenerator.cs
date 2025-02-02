namespace CRM.CoreService.Application.Interfaces.Infrastructure
{
    public interface IPasswordGenerator
    {
        string GeneratePassword(int length);
    }
}