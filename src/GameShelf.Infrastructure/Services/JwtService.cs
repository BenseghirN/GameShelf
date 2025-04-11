using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GameShelf.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            IConfigurationSection jwtSettings = _configuration.GetSection("JwtSettings");
            string secret = jwtSettings["Secret"] ?? throw new InvalidOperationException("Secret is not configured in JwtSettings.");
            string issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("Issuer is not configured in JwtSettings.");
            string audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("Audience is not configured in JwtSettings.");
            int expirationMinutes = Convert.ToInt32(jwtSettings["ExpirationMinutes"]);

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}