namespace TodoListApp.WebApi.Models.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using TodoListApp.WebApi.Models.UserModels;

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
