namespace TodoListApp.Services.Database.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("todo_item")]
    public class TodoItem
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("is_completed")]
        public bool IsCompleted { get; set; }

        [Column("due_date")]
        public DateTime? DueDate { get; set; }

        [Required]
        [Column("todo_list_id")]
        public int TodoListId { get; set; }

        // One-to-Many
        public TodoList TodoList { get; set; } = null!;
    }
}
