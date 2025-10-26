namespace TodoListApp.Infrastructure.Data.Database
{
    using Microsoft.EntityFrameworkCore;
    using TodoListApp.Entities.Entities;

    public class TodoListDbContext : DbContext
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }

        public DbSet<TodoList> TodoLists { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<UserTodoList> UserTodoLists { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ArgumentNullException.ThrowIfNull(modelBuilder);

            _ = modelBuilder.Entity<UserTodoList>()
                .HasKey(utl => new { utl.UserId, utl.TodoListId });

            _ = modelBuilder.Entity<UserGroup>()
                .HasKey(utl => new { utl.UserId, utl.GroupId });

            _ = modelBuilder.Entity<UserTodoList>()
                .HasOne(utl => utl.TodoList)
                .WithMany(tl => tl.UserTodoLists)
                .HasForeignKey(utl => utl.TodoListId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = modelBuilder.Entity<TodoList>()
                .HasOne(tl => tl.Group)
                .WithMany(g => g.TodoLists)
                .HasForeignKey(tl => tl.GroupId)
                .OnDelete(DeleteBehavior.SetNull);

            _ = modelBuilder.Entity<TodoItem>()
                .HasOne(ti => ti.TodoList)
                .WithMany(tl => tl.TodoItems)
                .HasForeignKey(ti => ti.TodoListId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = modelBuilder.Entity<Group>()
                .HasMany(g => g.TodoLists)
                .WithOne(tl => tl.Group)
                .HasForeignKey(tl => tl.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
