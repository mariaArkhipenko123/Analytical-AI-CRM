using CRM.CoreService.Domain.Entities;

namespace CRM.CoreService.Application.Interfaces.Infrastructure
{
    public interface IGoogleAuthService
    {
        Task<UserEntity> AuthenticateWithGoogleAsync(string idToken);
    }
}

