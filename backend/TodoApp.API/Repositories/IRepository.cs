namespace TodoApp.API.Repositories
{
    public interface IRepository
    {
        Task SaveChangesAsync();
    }

    public interface IRepository<T>: IRepository where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(Guid id);
    }
}
