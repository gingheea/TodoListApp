namespace TodoListApp.WebApi.Models.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TodoListApp.WebApi.Models.UserModels;

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
