using TodoApp.API.DTOs;
using TodoApp.API.Notifications;

namespace TodoApp.API.UseCases;

public interface ITaskService
{
    IReadOnlyCollection<Notification> Notifications { get; }
    Task<TaskReadDto?> CreateTaskAsync(TaskCreatedDto taskCreatedDto);
    Task<TaskReadDto?> UpdateTaskAsync(TaskUpdateDto taskUpdateDto, Guid taskId);
    Task DeleteTaskAsync(Guid taskId);
    Task<IEnumerable<TaskReadDto>> GetTasksByUserAsync(Guid userId);
    Task<IEnumerable<TaskReadDto>> GetTasksByCategoryAsync(string category);
    Task CompleteTaskAsync(Guid taskId);
}

