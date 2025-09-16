namespace TodoListApp.Services.Helpers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using TodoListApp.WebApi.Models.UserModels;

    public static class IdentityDataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roles = { "Admin", "User", "Owner", "Viewer", "Editor" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    _ = await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }

            var adminEmail = "admin@todo.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Nickname = "SuperAdmin",
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    _ = await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
