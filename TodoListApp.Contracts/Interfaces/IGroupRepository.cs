namespace TodoListApp.Contracts.Interfaces
{
    using TodoListApp.Entities.Entities;

    public interface IGroupRepository : IRepository<Group>
    {
        Task AddAsync(Group entity, int userId);

        Task<IEnumerable<Group>> GetAllAsync(int pageNumber, int rowCount, int userId);
    }
}
