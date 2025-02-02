using CRM.CoreService.Application.Interfaces.Infrastructure;
using CRM.CoreService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CRM.CoreService.Infrastructure.TokenProviders
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly SymmetricSecurityKey _key;
        private readonly double _expiersInMinutes;
        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"] ?? throw new ArgumentNullException("Jwt:Secret is null")));
            _expiersInMinutes = double.Parse(_configuration["Jwt:ExpiresInMinutes"] ?? throw new ArgumentNullException("Jwt:ExpiresInMinutes is null"));
        }
        public string GenerateAccessToken(UserEntity user, IEnumerable<Claim> userClaims)
        {
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            ArgumentNullException.ThrowIfNull(user.Email);
            List<Claim> fullClaims = new(userClaims)
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: fullClaims,
                expires: DateTime.UtcNow.AddMinutes(_expiersInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
        public string GenerateRefreshToken()
        {
            var refreshToken = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(refreshToken);
            }
            return Convert.ToBase64String(refreshToken);
        }
    }
}
