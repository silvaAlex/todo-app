
using Microsoft.EntityFrameworkCore;
using TodoApp.API.Data;
using TodoApp.API.Notifications;

namespace TodoApp.API.Repositories
{
    public abstract class BaseRepository<T>(TodoAppDbContext context) : IRepository<T> where T : class
    {
        protected DbSet<T> _dbSet = context.Set<T>();

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null) 
                _dbSet.Remove(entity);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
           var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
