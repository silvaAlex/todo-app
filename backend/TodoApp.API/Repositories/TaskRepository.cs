using TodoApp.Api.Models;
using TodoApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.API.Repositories;

public class TaskRepository(TodoAppDbContext context) : BaseRepository<TaskModel>(context), ITaskRepository
{
    public async Task<IEnumerable<TaskModel>> GetTasksByCategory(string category, Guid userId)
    {
        List<TaskModel> tasks;
        tasks = await _dbSet.Where(t => t.Category == category && t.UserId == userId).ToListAsync();

        return tasks ?? [];
    }

    public async Task<IEnumerable<TaskModel>> GetTasksByUserAsync(Guid userId)
    {
        List<TaskModel> tasks;
        tasks = await _dbSet.Where(t => t.UserId == userId).ToListAsync();

        return tasks ?? [];
    }

    public async Task CompleteTaskAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity != null)
        {
            entity.IsCompleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Category = "Done";

            Update(entity);
            await SaveChangesAsync();
        }
    }

    public async Task<TaskModel?> GetByIdAndUserAsync(Guid taskId, Guid userId)
    {
        var entity = await _dbSet
            .Where(x=>x.Id == taskId && x.UserId == userId)
            .FirstOrDefaultAsync();

        return entity;
    }
}

