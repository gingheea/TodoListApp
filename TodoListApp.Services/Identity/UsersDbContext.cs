namespace TodoListApp.Services.Identity
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using TodoListApp.WebApi.Models.UserModels;

    public class UsersDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }
    }
}
