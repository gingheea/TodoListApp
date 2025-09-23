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

        public async void Add(GroupModel item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                throw new ArgumentException("Group name cannot be empty");
            }

            var entity = this.mapper.Map<Group>(item);

            await this.repository.AddAsync(entity);
        }

        public async void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid group id");
            }

            await this.repository.DeleteByIdAsync(id);
        }

        public IEnumerable<GroupModel> GetAll(int pageNumber = 1, int rowCount = 0)
        {
            return this.mapper.Map<IEnumerable<GroupModel>>(this.repository.GetAllAsync(pageNumber, rowCount).Result);
        }

        public GroupModel GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid group id");
            }

            return this.mapper.Map<GroupModel>(this.repository.GetByIdAsync(id).Result);
        }

        public void Update(GroupModel item)
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

            _ = this.repository.UpdateAsync(entity);
        }
    }
}
