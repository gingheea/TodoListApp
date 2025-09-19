namespace TodoListApp.Services.Database
{
    using Microsoft.EntityFrameworkCore;
    using TodoListApp.Services.Database.Entities;

    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }

        public DbSet<TodoList> TodoLists { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<UserTodoList> UserTodoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            _ = modelBuilder.Entity<UserTodoList>()
                .HasKey(utl => new { utl.UserId, utl.TodoListId });

            _ = modelBuilder.Entity<UserTodoList>()
                .HasOne(utl => utl.TodoList)
                .WithMany(tl => tl.UserTodoLists)
                .HasForeignKey(utl => utl.TodoListId);

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
        }
    }
}
