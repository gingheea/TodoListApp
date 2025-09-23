namespace TodoListApp.WebApi.Controllers.AuthArea
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using TodoListApp.Contracts.DTO;
    using TodoListApp.Contracts.Interfaces;

    [ApiController]
    [Area("Auth")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto form)
        {

            try
            {
                var (success, response) = await this._authService.LoginAsync(form);
                return success ? this.Ok(response) : this.Unauthorized(response);
            }
            catch (InvalidOperationException ex)
            {
                return this.BadRequest(new { message = "Invalid operation: " + ex.Message });
            }
            catch (SecurityTokenException ex)
            {
                return this.Unauthorized(new { message = "Token error: " + ex.Message });
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] RegisterDto form)
        {
            try
            {
                var (success, response) = await this._authService.SignupAsync(form);
                return success ? this.Ok(response) : this.BadRequest(response);
            }
            catch (InvalidOperationException ex)
            {
                return this.BadRequest(new { message = "Invalid operation: " + ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return this.Unauthorized(new { message = "Invalud token" });
                }

                var (success, response) = await this._authService.LogoutAsync(userId);
                return success ? this.Ok(response) : this.BadRequest(response);
            }
            catch (InvalidOperationException ex)
            {
                return this.BadRequest(new { message = "Invalid Operation: " + ex.Message });
            }
            catch (DbUpdateException ex)
            {
                return this.StatusCode(500, new { message = "Server error: " + ex.Message });
            }
        }
    }
}
