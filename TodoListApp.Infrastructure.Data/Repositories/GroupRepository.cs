namespace TodoListApp.Infrastructure.Data.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TodoListApp.Contracts.Interfaces;
    using TodoListApp.Entities.Entities;
    using TodoListApp.Infrastructure.Data.Database;

    public class GroupRepository : IGroupRepository
    {
        private readonly TodoListDbContext context;

        public GroupRepository(TodoListDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Group entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            _ = await this.context.Groups.AddAsync(entity);
            _ = await this.context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Group entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            _ = this.context.Groups.Remove(entity);
            _ = await this.context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be a non-negative integer.");
            }

            var groupRemove = await this.context.Groups.FindAsync(id);

            if (groupRemove == null)
            {
                throw new KeyNotFoundException($"Group with ID {id} not found.");
            }

            _ = this.DeleteAsync(groupRemove);

        }

        public async Task<IEnumerable<Group>> GetAllAsync(int pageNumber, int rowCount)
        {
            return await this.context.Groups
               .Skip((pageNumber - 1) * rowCount)
               .Take(rowCount)
               .ToListAsync();
        }

        public async Task<Group?> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be a non-negative integer.");
            }

            return await this.context.Groups.FindAsync(id);
        }

        public async Task UpdateAsync(Group entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            var group = await this.context.Groups.FindAsync(entity.Id);

            if (group != null)
            {
                group.Name = entity.Name;
                group.CreatedByUserId = entity.CreatedByUserId;
                _ = await this.context.SaveChangesAsync();
            }

        }
    }
}
