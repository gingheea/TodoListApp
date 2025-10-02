namespace TodoListApp.Contracts.Interfaces
{
    using TodoListApp.Entities.Entities;

    public interface ITodoListRepository : IRepository<TodoList>
    {
        Task AddAsync(TodoList entity, int userId);

        Task<IEnumerable<TodoList>> GetAllAsync(int pageNumber, int rowCount, int userId);

        Task<TodoListRole?> GetUserRoleInListAsync(int userId, int listId);
    }
}
