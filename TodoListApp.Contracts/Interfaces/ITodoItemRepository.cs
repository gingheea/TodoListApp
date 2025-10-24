namespace TodoListApp.Contracts.Interfaces
{
    using TodoListApp.Entities.Entities;

    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetAllAsync(int pageNumber, int rowCount, int listId);

        Task<TodoListRole?> GetUserRoleInListAsync(int userId, int listId);

        Task ToggleCompleteAsync(int taskId, bool isCompleted);
    }
}
