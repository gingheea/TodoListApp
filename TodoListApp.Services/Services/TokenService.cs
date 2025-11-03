#pragma warning disable CS8604
#pragma warning disable CA1062
#pragma warning disable CA1305
namespace TodoListApp.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using TodoListApp.Contracts.Interfaces;
    using TodoListApp.Entities.Entities;

    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public TokenService(UserManager<User> userManager, IConfiguration config)
        {
            this._userManager = userManager;
            this._config = config;
        }

        public async Task<(string Token, DateTime ExpiresAtUtc)> CreateToken(User user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(this._config["Jwt:Key"]);
            if (!int.TryParse(this._config["Jwt:AccessTokenMinutes"], out int accessTokenMinutes) || accessTokenMinutes <= 0)
            {
                accessTokenMinutes = 120;
            }

            var expiresAtUtc = DateTime.UtcNow.AddMinutes(accessTokenMinutes);

            var claims = new List<Claim>
            {
             new Claim("Id", user.Id.ToString()),
             new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
             new Claim("Nickname", user.Nickname ?? string.Empty),
            };

            var roles = await this._userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAtUtc,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = this._config["Jwt:Issuer"],
                Audience = this._config["Jwt:Audience"],
            };

            var token = jwtHandler.CreateToken(tokenDescriptor);
            return (jwtHandler.WriteToken(token), expiresAtUtc);
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string? GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                return null;
            }

            var jwt = handler.ReadJwtToken(token);
            return jwt.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        }

        public string HashRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public bool ValidateRefreshToken(string refreshToken, string tokenHash)
        {
            throw new NotImplementedException();
        }
    }
}
