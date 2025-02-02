using CRM.CoreService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Google.Apis.Auth;
using CRM.CoreService.Application.Interfaces.Infrastructure;

namespace CRM.CoreService.Infrastructure.AuthenticationServices
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly UserManager<UserEntity> userManager;
        public GoogleAuthService(UserManager<UserEntity> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<UserEntity> AuthenticateWithGoogleAsync(string idToken)
        {
            var payload = await VerifyGoogleTokenAsync(idToken);
            if (payload == null)
            {
                throw new Exception("Invalid Google ID token");
            }
            var user = await userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new UserEntity
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    Issuer = payload.Issuer,
                };
                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create user");
                }
                return user;
            }
            else
            {
                if (payload.Issuer == user.Issuer) return user;
                throw new Exception("User has been already registered in another way");
            }
        }
        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string idToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
                return payload;
            }
            catch (Exception ex)
            {
                throw new Exception("Error validating Google ID token", ex);
            }
        }
    }
}
