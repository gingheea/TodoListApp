#pragma warning disable S4457
namespace TodoListApp.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TodoListApp.Contracts.Interfaces;
    using TodoListApp.Entities.Entities;
    using TodoListApp.WebApi.Models.Models;

    public class UserService : ICrud<UserModel>
    {
        private readonly IUserRepository repository;
        private readonly AutoMapper.IMapper mapper;

        public UserService(IUserRepository repository, AutoMapper.IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task Add(UserModel item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid user id");
            }

            return this.repository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync(int pageNumber = 1, int rowCount = 10)
        {
            var users = await this.repository.GetAllAsync(pageNumber, rowCount);
            return this.mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid user id");
            }

            var user = await this.repository.GetByIdAsync(id);

            return this.mapper.Map<UserModel>(user);
        }

        public async Task Update(UserModel item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (item.Id <= 0)
            {
                throw new ArgumentException("Invalid user id");
            }

            if (string.IsNullOrWhiteSpace(item.Nickname))
            {
                throw new ArgumentException("Nickname cannot be empty");
            }

            var entity = this.mapper.Map<User>(item);

            await this.repository.UpdateAsync(entity);
        }
    }
}
