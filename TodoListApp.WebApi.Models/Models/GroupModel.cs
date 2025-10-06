namespace TodoListApp.WebApi.Models.Models
{
    public class GroupModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int CreatedByUserId { get; set; }

        public ICollection<TodoListModel> TodoLists { get; set; } = new List<TodoListModel>();
    }
}
