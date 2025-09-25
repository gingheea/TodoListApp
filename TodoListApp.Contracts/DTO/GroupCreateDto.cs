namespace TodoListApp.Contracts.DTO
{
    public class GroupCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public int? CreatedByUserId { get; set; }
    }
}
