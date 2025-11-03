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

        public DbSet<RefreshToken> RefreshTokens => this.Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            _ = builder.Entity<RefreshToken>()
                .Property(token => token.TokenHash)
                .HasMaxLength(256)
                .IsRequired();

            _ = builder.Entity<RefreshToken>()
                .Property(token => token.ReplacedByToken)
                .HasMaxLength(256);

            _ = builder.Entity<RefreshToken>()
                .HasIndex(token => token.TokenHash)
                .IsUnique();

            _ = builder.Entity<RefreshToken>()
                .HasOne(token => token.User)
                .WithMany(user => user.RefreshTokens)
                .HasForeignKey(token => token.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
