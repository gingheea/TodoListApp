namespace TodoListApp.Contracts.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TodoListApp.Entities.Entities;

    public interface ITaskRepository
    {
        Task<IEnumerable<TodoItem>> GetUserTasksAsync(int userId);

        Task<IEnumerable<TodoItem>> GetUserTasksByStatusAsync(int userId, string status, int pageNumber, int rowCount);
    }
}
