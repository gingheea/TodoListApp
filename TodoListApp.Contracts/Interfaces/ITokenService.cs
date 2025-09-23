namespace TodoListApp.Contracts.Interfaces
{
    using TodoListApp.Entities.Entities;

    public interface ITokenService
    {
        Task<string> CreateToken(User user);
        string? GetUserIdFromToken(string token);
    }
}
