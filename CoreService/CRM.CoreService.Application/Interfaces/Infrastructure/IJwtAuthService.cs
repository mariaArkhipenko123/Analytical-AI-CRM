using CRM.CoreService.Application.Models.DTOs;
using CRM.CoreService.Domain.Entities;

namespace CRM.CoreService.Application.Interfaces.Infrastructure
{
    public interface IJwtAuthService
    {
        Task<TokensResponseDTO> LoginAsync(string email, string password);
        Task<TokensResponseDTO> RefreshTokens(Guid userId);
        Task<TokensResponseDTO> RegisterAsync(string email,string role = "User", string culture = "en");
        Task<TokensResponseDTO> LoginWithGoogleAsync(string idToken);
        Task ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);
    }
}

