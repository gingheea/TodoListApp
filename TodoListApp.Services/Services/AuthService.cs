namespace TodoListApp.Services.Services
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using TodoListApp.Contracts.DTO;
    using TodoListApp.Contracts.Interfaces;
    using TodoListApp.Entities.Entities;

    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService tokenService,
            RoleManager<IdentityRole<int>> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenService = tokenService;
            this._roleManager = roleManager;
        }

        public object GetAllRoles()
        {
            return this._roleManager.Roles
                .Select(r => new { r.Id, r.Name })
                .ToList();
        }

        public async Task<(bool Success, object Response)> LoginAsync(LoginDto form)
        {
            User? user;
            if (form.EmailOrUsername.Contains('@'))
            {
                user = await this._userManager.FindByEmailAsync(form.EmailOrUsername);
            }
            else
            {
                user = await this._userManager.Users.FirstOrDefaultAsync(u => u.UserName == form.EmailOrUsername);
            }

            if (user == null)
            {
                return (false, new { message = "User not found" });
            }

            var result = await this._signInManager.CheckPasswordSignInAsync(user, form.Password, false);
            if (!result.Succeeded)
            {
                return (false, new { message = "Invalid password" });
            }

            string token = await this._tokenService.CreateToken(user);
            return (true, new { token, message = "Successful login" });
        }

        public async Task<(bool Success, object Response)> LogoutAsync(int userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString(CultureInfo.InvariantCulture));
            if (user == null)
            {
                return (false, new { message = "User not found" });
            }

            user.SecurityStamp = Guid.NewGuid().ToString();
            _ = await this._userManager.UpdateAsync(user);

            return (true, new { message = "Successful logout" });
        }

        public async Task<(bool Success, object Response)> RefreshJwtToken(int userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString(CultureInfo.InvariantCulture));
            if (user == null)
            {
                return (false, new { message = "User not found" });
            }

            string token = await this._tokenService.CreateToken(user);
            return (true, new { token, message = "Token refreshed successfully" });
        }

        public async Task<(bool Success, object Response)> SignupAsync(RegisterDto form)
        {
            var existingUserEmail = await this._userManager.FindByEmailAsync(form.Email);
            if (existingUserEmail != null)
            {
                return (false, new { message = "A user with this email already exists" });
            }

            var existingUserUsername = await this._userManager.FindByNameAsync(form.Nickname);
            if (existingUserUsername != null)
            {
                return (false, new { message = "A user with this nickname already exists" });
            }

            var user = new User
            {
                UserName = form.Nickname,
                Email = form.Email,
                Nickname = form.Nickname,
            };

            var result = await this._userManager.CreateAsync(user, form.Password);
            if (!result.Succeeded)
            {
                return (false, new { message = string.Join("; ", result.Errors.Select(e => e.Description)) });
            }

            _ = await this._userManager.AddToRoleAsync(user, "user");

            string token = await this._tokenService.CreateToken(user);
            return (true, new { token, message = "User created successfully" });
        }
    }
}
