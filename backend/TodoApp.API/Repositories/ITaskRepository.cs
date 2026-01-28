namespace TodoApp.API.Repositories;
using TodoApp.Api.Models;

public interface ITaskRepository
{
    Task<IEnumerable<TaskModel>> GetTasksByUserAsync(Guid userId);
    Task<IEnumerable<TaskModel>> GetTasksByCategory(string category);
}

