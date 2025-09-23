namespace TodoListApp.Entities.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("todo_list")]
    public class TodoList
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Title { get; set; } = string.Empty;

        [Column("group_id")]
        public int? GroupId { get; set; }

        // One-to-many
        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();

        // Many-to-Many
        public ICollection<UserTodoList> UserTodoLists { get; set; } = new List<UserTodoList>();

        public Group? Group { get; set; }
    }
}
