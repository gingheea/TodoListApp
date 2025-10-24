namespace TodoListApp.WebApp.Model
{
    public class TodoItemCreateModel
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
