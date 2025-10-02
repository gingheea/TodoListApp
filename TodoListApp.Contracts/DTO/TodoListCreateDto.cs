namespace TodoListApp.Contracts.DTO
{
    public class TodoListCreateDto
    {
        public string Title { get; set; } = string.Empty;

        public int? GroupId { get; set; } = null;
    }
}
