
namespace TodoListApp.Contracts.Interfaces
{
    using System;
    using TodoListApp.Entities.Entities;

    public interface ITokenService
    {
        Task<(string Token, DateTime ExpiresAtUtc)> CreateToken(User user);

        string GenerateRefreshToken();

        string HashRefreshToken(string refreshToken);

        bool ValidateRefreshToken(string refreshToken, string tokenHash);

        string? GetUserIdFromToken(string token);
    }
}
