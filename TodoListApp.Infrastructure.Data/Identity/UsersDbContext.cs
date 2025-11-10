namespace TodoListApp.Infrastructure.Data.Identity
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using TodoListApp.Entities.Entities;

    public class UsersDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }
    }
}
