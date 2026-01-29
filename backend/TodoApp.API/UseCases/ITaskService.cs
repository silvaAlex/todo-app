using TodoApp.API.DTOs;
using TodoApp.API.Notifications;

namespace TodoApp.API.UseCases;

public interface ITaskService
{
    IReadOnlyCollection<Notification> Notifications { get; }
    Task<TaskReadDto?> GetTaskByIdAndUserAsync(Guid taskId, Guid userId);
    Task<TaskReadDto?> CreateTaskAsync(TaskCreatedDto taskCreatedDto);
    Task<TaskReadDto?> UpdateTaskAsync(TaskUpdateDto taskUpdateDto, Guid taskId, Guid userId);
    Task DeleteTaskAsync(Guid taskId, Guid userId);
    Task<IEnumerable<TaskReadDto>> GetTasksByUserAsync(Guid userId);
    Task<IEnumerable<TaskReadDto>> GetTasksByCategoryAsync(string category, Guid userId);
    Task CompleteTaskAsync(Guid taskId);
}

