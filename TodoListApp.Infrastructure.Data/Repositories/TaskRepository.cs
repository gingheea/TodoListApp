namespace TodoListApp.Infrastructure.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TodoListApp.Contracts.Interfaces;
    using TodoListApp.Entities.Entities;
    using TodoListApp.Infrastructure.Data.Database;

    public class TaskRepository : ITaskRepository
    {
        private readonly TodoListDbContext context;

        public TaskRepository(TodoListDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetUserTasksAsync(int userId)
        {
            return await this.context.TodoItems
                .Include(t => t.TodoList)
                    .ThenInclude(l => l.UserTodoLists)
                .Where(t => t.TodoList.UserTodoLists
                    .Any(utl => utl.UserId == userId))
                .ToListAsync();
        }
    }
}
