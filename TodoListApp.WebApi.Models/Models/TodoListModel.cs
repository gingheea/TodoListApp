namespace TodoListApp.WebApi.Models.Models
{
    public class TodoListModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        private readonly List<TodoItemModel> items = new();

        public IReadOnlyCollection<TodoItemModel> Items => this.items.AsReadOnly();
    }
}
