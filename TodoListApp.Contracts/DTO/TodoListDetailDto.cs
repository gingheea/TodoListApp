namespace TodoListApp.Contracts.DTO
{
    public class TodoListDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public List<TodoItemDto> Items { get; set; } = new();
    }
}
