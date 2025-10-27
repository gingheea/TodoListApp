#pragma warning disable S4457
namespace TodoListApp.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TodoListApp.Contracts.Interfaces;
    using TodoListApp.Entities.Entities;
    using TodoListApp.WebApi.Models.Models;

    public class TodoListService : ICrud<TodoListModel>
    {
        private readonly ITodoListRepository repository;
        private readonly AutoMapper.IMapper mapper;

        public TodoListService(ITodoListRepository repository, AutoMapper.IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task Add(TodoListModel item, int userId)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (string.IsNullOrWhiteSpace(item.Title))
            {
                throw new ArgumentException("Todo list name cannot be empty");
            }

            var entity = this.mapper.Map<TodoList>(item);

            await this.repository.AddAsync(entity, userId);
        }

        public Task Add(TodoListModel item)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid todo list id");
            }

            await this.repository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<TodoListModel>> GetAllAsync(int userId, int? groupId, int pageNumber = 1, int rowCount = 10)
        {
            var todoLists = await this.repository.GetAllAsync(pageNumber, rowCount, userId, groupId);
            return this.mapper.Map<IEnumerable<TodoListModel>>(todoLists);
        }

        public async Task<IEnumerable<TodoListModel>> GetAllAsync(int pageNumber = 1, int rowCount = 10)
        {
            var todoLists = await this.repository.GetAllAsync(pageNumber, rowCount);
            return this.mapper.Map<IEnumerable<TodoListModel>>(todoLists);
        }

        public async Task<TodoListModel> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid todo list id");
            }

            var todoList = await this.repository.GetByIdAsync(id);

            return this.mapper.Map<TodoListModel>(todoList);
        }

        public async Task Update(TodoListModel item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (string.IsNullOrEmpty(item.Title))
            {
                throw new ArgumentException("Invalid todo list id");
            }

            var entity = this.mapper.Map<TodoList>(item);

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
    }
}
