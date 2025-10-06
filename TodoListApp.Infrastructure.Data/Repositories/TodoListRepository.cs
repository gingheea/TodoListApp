namespace TodoListApp.Infrastructure.Data.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TodoListApp.Contracts.Interfaces;
    using TodoListApp.Entities.Entities;
    using TodoListApp.Infrastructure.Data.Database;

    public class TodoListRepository : ITodoListRepository
    {
        private readonly TodoListDbContext context;

        public TodoListRepository(TodoListDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(TodoList entity, int userId)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            _ = await this.context.TodoLists.AddAsync(entity);
            _ = await this.context.SaveChangesAsync();

            var link = new UserTodoList
            {
                UserId = userId,
                TodoListId = entity.Id,
                Role = TodoListRole.Owner,
            };

            _ = await this.context.UserTodoLists.AddAsync(link);
            _ = await this.context.SaveChangesAsync();
        }

        public Task AddAsync(TodoList entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(TodoList entity)
        {
            _ = this.context.TodoLists.Remove(entity);
            _ = await this.context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be a non-negative integer.");
            }

            var todoListRemove = await this.context.TodoLists.FindAsync(id);

            if (todoListRemove == null)
            {
                throw new KeyNotFoundException($"TodoList with ID {id} not found.");
            }

            await this.DeleteAsync(todoListRemove);
        }

        public async Task<IEnumerable<TodoList>> GetAllAsync(int pageNumber, int rowCount)
        {
            return await this.context.TodoLists
               .Skip((pageNumber - 1) * rowCount)
               .Take(rowCount)
               .ToListAsync();
        }

        public async Task<IEnumerable<TodoList>> GetAllAsync(int pageNumber, int rowCount, int userId, int? groupId)
        {
            var query = this.context.UserTodoLists
                .Where(x => x.UserId == userId)
                .Select(x => x.TodoList)
                .AsQueryable();

            if (groupId.HasValue)
            {
                query = query.Where(tl => tl.GroupId == groupId.Value);
            }

            return await query
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount)
                .ToListAsync();
        }

        public async Task<TodoList?> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be a non-negative integer.");
            }

            return await this.context.TodoLists
                 .Include(tl => tl.TodoItems)
                 .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task UpdateAsync(TodoList entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            var todoList = await this.context.TodoLists.FindAsync(entity.Id);

            if (todoList != null)
            {
                todoList.Title = entity.Title;
                _ = await this.context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Todo List with ID {entity.Id} not found.");
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
