using CRM.CoreService.Application.Interfaces.Infrastructure;
using CRM.CoreService.Application.Models.DTOs;
using CRM.CoreService.Domain.Entities;
using CRM.CoreService.Domain.Enums;
using CRM.CoreService.Infrastructure.Exceptions;
using CRM.CoreService.Infrastructure.Extensions.Identity;
using CRM.CoreService.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace CRM.CoreService.Infrastructure.AuthenticationServices
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly ITokenProvider _tokenProvider;
        private readonly ICacheService _cache;
        private readonly IConfiguration _configuration;
        private readonly IGoogleAuthService _googleAuthService;
        private readonly IEmailMessageSender _emailSender;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly EmailMessageBuilder _emailMessageBuilder;
        public JwtAuthService(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager,
            ITokenProvider tokenProvider, ICacheService cache, 
            IConfiguration configuration, IGoogleAuthService googleAuthService, 
            IEmailMessageSender emailSender, IPasswordGenerator passwordGenerator, EmailMessageBuilder emailMessageBuilder)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenProvider = tokenProvider;
            _cache = cache;
            _configuration = configuration;
            _googleAuthService = googleAuthService;
            _emailSender = emailSender;
            _passwordGenerator = passwordGenerator;
            _emailMessageBuilder = emailMessageBuilder;
        }
        public async Task<TokensResponseDTO> RegisterAsync(string email, string role = "User", string culture = "en")
        {
            string temporaryPassword = null;

            var user = new UserEntity()
            {
                Email = email,
                UserName = email,
                Status = UserStatus.Pending,
                IsTemporaryPassword = true
            };

            temporaryPassword = _passwordGenerator.GeneratePassword(8);

            var regResult = await _userManager.CreateAsync(user, temporaryPassword);
            if (!regResult.Succeeded)
                throw new Exception(regResult.Errors.FirstOrDefault()?.Description);

            var roleResult = await AddRoleToUser(user.Email, role);
            if (!roleResult.Succeeded)
                throw new Exception(roleResult.Errors.FirstOrDefault()?.Description);

            var claims = await _userManager.GetRoleClaimsForUserAsync(_roleManager, user);
            var logs = new UserLogsDTO
            {
                UserId = user.Id,
                Action = "Register",
                IP = "default"
            };

            //send userLogsData to MessageBroker

            var messageDTO = _emailMessageBuilder.BuildRegistrationMessage(email, temporaryPassword, culture);
            await _emailSender.SendEmailAsync(user.Email, messageDTO);

            return await GenerateTokens(user, claims);
        }

        public async Task<TokensResponseDTO> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user.Issuer != null)
                throw new Exception("User has been already registered in another way");
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                var unsuccessfulCaseLogs = new UserLogsDTO
                {
                    Action = "Unsuccessful login",
                    IP = "default"
                };
                //send userLogsData to MessageBroker
                throw new Exception("User not found or password is incorrect!");
            }
            if (user.IsTemporaryPassword == true)
                throw new TemporaryPasswordException();
            var claims = await _userManager.GetRoleClaimsForUserAsync(_roleManager, user);
            var successfulCaseLogs = new UserLogsDTO
            {
                UserId = user.Id,
                Action = "Login",
                IP = "default"
            };
            //send userLogsData to MessageBroker
            return await GenerateTokens(user, claims);
        }
        public async Task<TokensResponseDTO> LoginWithGoogleAsync(string idToken)
        {
            var user = await _googleAuthService.AuthenticateWithGoogleAsync(idToken);
            var roleResult = await AddRoleToUser(user.Email, "User");
            if (!roleResult.Succeeded)
                throw new Exception(roleResult.Errors.FirstOrDefault()?.Description);
            var claims = await _userManager.GetRoleClaimsForUserAsync(_roleManager, user);
            var logs = new UserLogsDTO
            {
                UserId = user.Id,
                Action = "LoginWithGoogle",
                IP = "default"
            };
            //send userLogsData to MessageBroker
            return await GenerateTokens(user, claims);
        }
        public async Task ChangePasswordAsync(UserEntity user, string oldPassword,string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Password change failed");
            }
            user.IsTemporaryPassword = false;
            var logs = new UserLogsDTO
            {
                UserId = user.Id,
                Action = "PasswordChange",
                IP = "default"
            };
            //send userLogsData to MessageBroker
            await _userManager.UpdateAsync(user);
        }
        private async Task<TokensResponseDTO> GenerateTokens(UserEntity user, IEnumerable<Claim> claims)
        {
            var accessToken = _tokenProvider.GenerateAccessToken(user, claims);
            var refreshToken = _tokenProvider.GenerateRefreshToken();

            var refreshTokenSliding = TimeSpan.FromMinutes(int.Parse(_configuration["Jwt:SlidingExpiration"]));
            var refreshTokenAbsolute = TimeSpan.FromMinutes(int.Parse(_configuration["Jwt:AbsoluteExpiration"]));

            await _cache.SetAsync<string>(user.Id.ToString(), refreshToken, refreshTokenSliding, refreshTokenAbsolute);
            return new TokensResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
        private async Task<IdentityResult> AddRoleToUser(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            if (!await _roleManager.RoleExistsAsync(role)) return IdentityResult.Failed(new IdentityError { Description = "Role not found" });
            return await _userManager.AddToRoleAsync(user, role);
        }
        public async Task<TokensResponseDTO> RefreshTokens(Guid userId)
        {
            var refreshToken = await _cache.GetAsync<string>(userId.ToString());
            if (refreshToken == null)
            {
                throw new Exception("Invalid userId or token has been expired");
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new Exception("User not found");
            }
            await _cache.RemoveAsync(refreshToken);
            var claims = await _userManager.GetRoleClaimsForUserAsync(_roleManager, user);
            return await GenerateTokens(user, claims);
        }

    }
}
