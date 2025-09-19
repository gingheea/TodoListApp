namespace TodoListApp.Services.Database.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("group")]
    public class Group
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("created_by_user_id")]
        public int CreatedByUserId { get; set; }

        public ICollection<TodoList> TodoLists { get; set; } = new List<TodoList>();
    }
}
