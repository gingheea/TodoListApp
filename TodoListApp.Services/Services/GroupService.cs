namespace TodoListApp.Services.Services
{
    using System.Collections.Generic;
    using TodoListApp.Contracts.Interfaces;
    using TodoListApp.Entities.Entities;
    using TodoListApp.WebApi.Models.Models;

    public class GroupService : ICrud<GroupModel>
    {
        private readonly IGroupRepository repository;
        private readonly AutoMapper.IMapper mapper;

        public GroupService(IGroupRepository repository, AutoMapper.IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task Add(GroupModel item, int userId)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                throw new ArgumentException("Group name cannot be empty");
            }

            var entity = this.mapper.Map<Group>(item);

            await this.repository.AddAsync(entity, userId);
        }

        public Task Add(GroupModel item)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid group id");
            }

            await this.repository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<GroupModel>> GetAllAsync(int userId, int pageNumber = 1, int rowCount = 10)
        {
            var groups = await this.repository.GetAllAsync(pageNumber, rowCount, userId);
            return this.mapper.Map<IEnumerable<GroupModel>>(groups);
        }

        public async Task<IEnumerable<GroupModel>> GetAllAsync(int pageNumber = 1, int rowCount = 10)
        {
            var groups = await this.repository.GetAllAsync(pageNumber, rowCount);
            return this.mapper.Map<IEnumerable<GroupModel>>(groups);
        }

        public async Task<GroupModel> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid group id");
            }

            var group = await this.repository.GetByIdAsync(id);
            return this.mapper.Map<GroupModel>(group);
        }

        public async Task Update(GroupModel item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (item.Id <= 0)
            {
                throw new ArgumentException("Invalid group id");
            }

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                throw new ArgumentException("Group name cannot be empty");
            }

            var entity = this.mapper.Map<Group>(item);

            await this.repository.UpdateAsync(entity);
        }
    }
}
