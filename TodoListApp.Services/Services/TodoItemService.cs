#pragma warning disable S4457
namespace TodoListApp.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TodoListApp.Contracts.Interfaces;
    using TodoListApp.Entities.Entities;
    using TodoListApp.WebApi.Models.Models;

    public class TodoItemService : ICrud<TodoItemModel>
    {
        private readonly ITodoItemRepository repository;
        private readonly AutoMapper.IMapper mapper;

        public TodoItemService(ITodoItemRepository repository, AutoMapper.IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task Add(TodoItemModel item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (string.IsNullOrWhiteSpace(item.Title))
            {
                throw new ArgumentException("Group name cannot be empty");
            }

            if (item.TodoListId < 0)
            {
                throw new ArgumentException("Invalid TodoListId");
            }

            var entity = this.mapper.Map<TodoItem>(item);

            await this.repository.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid todo item id");
            }

            await this.repository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<TodoItemModel>> GetAllAsync(int pageNumber = 1, int rowCount = 10)
        {
            var todoItems = await this.repository.GetAllAsync(pageNumber, rowCount);
            return this.mapper.Map<IEnumerable<TodoItemModel>>(todoItems);
        }

        public async Task<IEnumerable<TodoItemModel>> GetAllAsync(int listId, int pageNumber = 1, int rowCount = 10)
        {
            var todoItems = await this.repository.GetAllAsync(pageNumber, rowCount, listId);
            return this.mapper.Map<IEnumerable<TodoItemModel>>(todoItems);
        }

        public async Task<TodoItemModel> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid todo item id");
            }

            var todoItem = await this.repository.GetByIdAsync(id);
            return this.mapper.Map<TodoItemModel>(todoItem);
        }

        public async Task Update(TodoItemModel item)
        {
            ArgumentNullException.ThrowIfNull(item);
            if (item.Id <= 0)
            {
                throw new ArgumentException("Invalid todo item id");
            }

            var entity = this.mapper.Map<TodoItem>(item);
            await this.repository.UpdateAsync(entity);
        }

        public async Task<TodoListRole?> GetUserRoleInListAsync(int userId, int listId)
        {
            if (userId <= 0 || listId <= 0)
            {
                throw new ArgumentException("Invalid user id");
            }

            return await this.repository.GetUserRoleInListAsync(userId, listId);
        }

        public async Task ToggleCompleteAsync(int taskId, bool isCompleted)
        {
            if (taskId <= 0)
            {
                throw new ArgumentException("Invalid task id");
            }

            await this.repository.ToggleCompleteAsync(taskId, isCompleted);
        }
    }
}
