namespace TodoListApp.Contracts.DTO
{
    public class DashboardDto
    {
        public string UserName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public int TasksDue { get; set; }

        public double TotalCount { get; set; }

        public double CompletionPercent { get; set; }
    }
}
