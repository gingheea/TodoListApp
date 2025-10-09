namespace TodoListApp.Contracts.Interfaces
{
    public interface ICrud<T>
    {
        Task<IEnumerable<T>> GetAllAsync(int pageNumber = 1, int rowCount = 10);

        Task<T> GetById(int id);

        Task Add(T item);

        Task Update(T item);

        Task Delete(int id);
    }
}
