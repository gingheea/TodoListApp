namespace TodoListApp.Contracts.Interfaces
{
    using TodoListApp.Contracts.DTO;
    public interface IAuthService
    {
        Task<(bool Success, object Response)> SignupAsync(RegisterDto form);
        Task<(bool Success, object Response)> LoginAsync(LoginDto form);
        Task<(bool Success, object Response)> LogoutAsync(int userId);
        Task<(bool Success, object Response)> RefreshJwtToken(int userId);
        object GetAllRoles();
    }
}
