namespace TodoListApp.Contracts.DTO
{
    using System;

    public class RefreshTokenResponseDto
    {
        public string Message { get; set; } = string.Empty;

        public string? Token { get; set; }

        public DateTime? TokenExpiresAt { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiresAt { get; set; }

        public bool ForceLogout { get; set; }
    }
}
