namespace TodoListApp.Contracts.Interfaces
{
    using TodoListApp.Entities.Entities;

    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        Task<TodoListRole?> GetUserRoleInListAsync(int userId, int listId);
    }
}
