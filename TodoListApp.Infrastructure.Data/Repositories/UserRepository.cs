namespace TodoListApp.Infrastructure.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using TodoListApp.Contracts.Interfaces;
    using TodoListApp.Entities.Entities;
    using TodoListApp.Infrastructure.Data.Identity;

    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext context;

        public UserRepository(UsersDbContext context)
        {
            this.context = context;
        }

        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            _ = this.context.Users.Remove(entity);
            _ = await this.context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be a non-negative integer.");
            }

            var userRemove = await this.context.Users.FindAsync(id);

            if (userRemove == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            await this.DeleteAsync(userRemove);
        }

        public async Task<IEnumerable<User>> GetAllAsync(int pageNumber, int rowCount)
        {
            return await this.context.Users
               .Skip((pageNumber - 1) * rowCount)
               .Take(rowCount)
               .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be a non-negative integer.");
            }

            return await this.context.Users.FindAsync(id).AsTask();
        }

        public async Task UpdateAsync(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            var user = await this.context.Users.FindAsync(entity.Id);

            if (user != null)
            {
                user.Nickname = entity.Nickname;
                _ = await this.context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"User with ID {entity.Id} not found.");
            }
        }
    }
}
