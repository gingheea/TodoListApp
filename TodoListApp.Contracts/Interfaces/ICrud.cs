namespace TodoListApp.Contracts.Interfaces
{
    public interface ICrud<T>
    {
        IEnumerable<T> GetAll(int pageNumber = 1, int rowCount = 0);
        T GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
