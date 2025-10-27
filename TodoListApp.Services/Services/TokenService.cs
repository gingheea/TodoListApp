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

        public async Task<string> CreateToken(User user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(this._config["Jwt:Key"]);

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
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = this._config["Jwt:Issuer"],
                Audience = this._config["Jwt:Audience"],
            };

            var token = jwtHandler.CreateToken(tokenDescriptor);
            return jwtHandler.WriteToken(token);
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
    }
}
