namespace TodoListApp.Services.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TodoListApp.Contracts.Interfaces;
    using TodoListApp.WebApi.Models.Models;

    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly AutoMapper.IMapper mapper;

        public TaskService(ITaskRepository taskRepository, AutoMapper.IMapper mapper)
        {
            this._taskRepository = taskRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemModel>> GetUserTasksAsync(int userId)
        {
            var tasks = await this._taskRepository.GetUserTasksAsync(userId);

            return this.mapper.Map<IEnumerable<TodoItemModel>>(tasks);
        }
    }
}
