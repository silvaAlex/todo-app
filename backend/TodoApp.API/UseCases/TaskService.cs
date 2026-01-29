using TodoApp.Api.Models;
using TodoApp.API.DTOs;
using TodoApp.API.Notifications;
using TodoApp.API.Repositories;

namespace TodoApp.API.UseCases
{
    public class TaskService(ITaskRepository repository, DomainNotifier notifier) : ITaskService
    {
        public IReadOnlyCollection<Notification> Notifications => notifier.Notifications;

        public async Task CompleteTaskAsync(Guid taskId)
        {
            await repository.CompleteTaskAsync(taskId);
        }

        public async Task<TaskReadDto?> CreateTaskAsync(TaskCreatedDto taskCreatedDto)
        {
            var task = new TaskModel
            {
                Title = taskCreatedDto.Title,
                Description = taskCreatedDto.Description,
                Category = taskCreatedDto.Category,
                UserId = taskCreatedDto.UserId,
            };

            await repository.AddAsync(task);
            await repository.SaveChangesAsync();

            return new TaskReadDto(task.Id, task.UserId, task.Title, task.Description, task.Category, task.IsCompleted, task.CreatedAt, task.UpdatedAt);
        }

        public async Task DeleteTaskAsync(Guid taskId)
        {
            await repository.DeleteAsync(taskId);
        }

        public async Task<TaskReadDto?> GetTaskByIdAsync(Guid taskId)
        {
            var task = await repository.GetByIdAsync(taskId);

            if(task != null)
                return new TaskReadDto(task.Id, task.UserId, task.Title, task.Description, task.Category, task.IsCompleted, task.CreatedAt, task.UpdatedAt);

            return null;
        }

        public async Task<IEnumerable<TaskReadDto>> GetTasksByCategoryAsync(string category, Guid userId)
        {
            var tasks = await repository.GetTasksByCategory(category, userId);
            return tasks.Select(t => new TaskReadDto(t.Id, t.UserId, t.Title, t.Description, t.Category, t.IsCompleted, t.CreatedAt, t.UpdatedAt));
        }

        public async Task<IEnumerable<TaskReadDto>> GetTasksByUserAsync(Guid userId)
        {
            var tasks = await repository.GetTasksByUserAsync(userId);
            return tasks.Select(t => new TaskReadDto(t.Id, t.UserId, t.Title,t.Description, t.Category,t.IsCompleted, t.CreatedAt, t.UpdatedAt));
        }

        public async Task<TaskReadDto?> UpdateTaskAsync(TaskUpdateDto taskUpdateDto, Guid taskId)
        {
            var task = await repository.GetByIdAsync(taskId);

            if (task == null)
            {
                notifier.AddNotification(new Notification("TaskNotFound", $"a task com {taskId} não foi encontrada"));
                return null;
            }

            task.Title = taskUpdateDto.Title;
            task.Description = taskUpdateDto.Description;
            task.Category = taskUpdateDto.Category;
            task.UpdatedAt = DateTime.UtcNow;

            repository.Update(task);
            await repository.SaveChangesAsync();

            return new TaskReadDto(task.Id, task.UserId, task.Title, task.Description, task.Category, task.IsCompleted, task.CreatedAt, task.UpdatedAt);
        }
    }
}
