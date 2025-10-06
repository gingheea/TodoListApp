namespace TodoListApp.Entities.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("user_groups")]
    public class UserGroup
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("group_id")]
        public int GroupId { get; set; }

        public Group Group { get; set; } = null!;
    }
}
