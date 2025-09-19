namespace TodoListApp.Services.Database.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    public enum TodoListRole
    {
        Owner,
        Editor,
        Viewer,
    }

    [Table("user_todo_list")]
    public class UserTodoList
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("todo_list_id")]
        public int TodoListId { get; set; }

        [Column("role")]
        public TodoListRole Role { get; set; } = TodoListRole.Owner;

        public TodoList TodoList { get; set; } = null!;
    }
}
