using CRM.CoreService.Domain.Entities;
using System.Security.Claims;

namespace CRM.CoreService.Application.Interfaces.Infrastructure
{
    public interface ITokenProvider
    {
        string GenerateAccessToken(UserEntity user, IEnumerable<Claim> userClaims);
        string GenerateRefreshToken();
    }
}