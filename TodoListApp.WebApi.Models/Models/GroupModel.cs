namespace TodoListApp.WebApi.Models.Models
{
    public class GroupModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int CreatedByUserId { get; set; }

        private readonly List<TodoListModel> todoLists = new();

        public IReadOnlyCollection<TodoListModel> TodoLists => this.todoLists.AsReadOnly();
    }
}
