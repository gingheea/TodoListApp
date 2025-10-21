namespace TodoListApp.WebApp.Interfaces;

public interface IAuthService
{
    Task<(bool Success, string? Error)> LoginAsync(string emailOrUsername, string password);
    Task<(bool Success, string? Error)> RegisterAsync(string email, string nickname, string password, string confirmPassword);
    Task LogoutAsync();
    Task<string?> GetTokenAsync();
}
