namespace TodoListApp.Infrastructure.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using TodoListApp.Contracts.Interfaces;
    using TodoListApp.Entities.Entities;
    using TodoListApp.Infrastructure.Data.Database;

    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoListDbContext context;

        public TodoItemRepository(TodoListDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(TodoItem entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            _ = await this.context.TodoItems.AddAsync(entity);
            _ = await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TodoItem entity)
        {
            _ = this.context.TodoItems.Remove(entity);
            _ = await this.context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be a non-negative integer.");
            }

            var todoItemRemove = await this.context.TodoItems.FindAsync(id);

            if (todoItemRemove == null)
            {
                throw new KeyNotFoundException($"TodoItem with ID {id} not found.");
            }

            await this.DeleteAsync(todoItemRemove);
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync(int pageNumber, int rowCount)
        {
            return await this.context.TodoItems
               .Skip((pageNumber - 1) * rowCount)
               .Take(rowCount)
               .ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be a non-negative integer.");
            }

            return await this.context.TodoItems.FindAsync(id);
        }

        public async Task UpdateAsync(TodoItem entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            var todoItem = await this.context.TodoItems.FindAsync(entity.Id);

            if (todoItem != null)
            {
                todoItem.Title = entity.Title;
                todoItem.DueDate = entity.DueDate;
                todoItem.IsCompleted = entity.IsCompleted;
                todoItem.Description = entity.Description;
                _ = await this.context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Todo Item with ID {entity.Id} not found.");
            }
        }

        public async Task<TodoListRole?> GetUserRoleInListAsync(int userId, int listId)
        {
            var userList = await this.context.UserTodoLists
                .FirstOrDefaultAsync(x => x.UserId == userId && x.TodoListId == listId);

            return userList?.Role;
        }
    }
}
