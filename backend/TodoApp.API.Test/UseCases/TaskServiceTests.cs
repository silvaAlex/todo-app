using Moq;
using TodoApp.Api.Models;
using TodoApp.API.DTOs;
using TodoApp.API.Notifications;
using TodoApp.API.Repositories;
using TodoApp.API.UseCases;

namespace TodoApp.API.Test.UseCases;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _taskRepoMock = new();
    private readonly DomainNotifier _notifier = new();
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _taskService = new TaskService(_taskRepoMock.Object, _notifier);
    }

    [Fact]
    public async Task CompleteTaskAsync_ShouldCallRepository_WhenTaskIdProvided()
    {
        //Arrange
        var taskId = Guid.NewGuid();


        //Act
        await _taskService.CompleteTaskAsync(taskId);

        //Assert
        _taskRepoMock.Verify(x => x.CompleteTaskAsync(taskId), Times.Once);
    }

    [Fact]
    public async Task DeleteTaskAsync_ShouldCallRepository_WhenTaskIdProvided()
    {
        //Arrange
        var taskId = Guid.NewGuid();

        //Act
        await _taskService.DeleteTaskAsync(taskId);

        //Assert
        _taskRepoMock.Verify(x => x.DeleteAsync(taskId), Times.Once);
    }

    [Fact]
    public async Task GetTasksByUserAsync_ShouldReturnTasks_WhenUserHasTasks()
    {
        //Arrange
        var user = new User()
        {
            UserName = "alice",
            PasswordHash = "123456789"
        };

        var tasks = new List<TaskModel>()
        {
            new()
            {
                User = user,
                UserId = user.Id,
                Title  = "Tarefa 1",
                Description = "Description 1",
                Category = "ToDo",
                IsCompleted = false,
            },
            new()
            {
                User = user,
                UserId = user.Id,
                Title  = "Tarefa 2",
                Description = "Description 2",
                Category = "InProgress",
                IsCompleted = false,
            }
        };

        _taskRepoMock.Setup(x => x.GetTasksByUserAsync(user.Id)).ReturnsAsync(tasks);

        //Act
        var result = await _taskService.GetTasksByUserAsync(user.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, task => Assert.Equal(user.Id, task.UserId));
        _taskRepoMock.Verify(x => x.GetTasksByUserAsync(user.Id), Times.Once());
    }

    [Fact]
    public async Task UpdateTaskAsync_ShouldUpdateTask_WhenTaskExists()
    {
        //Arrange
        var user = new User()
        {
            UserName = "alice",
            PasswordHash = "123456789"
        };

        var taksId = Guid.NewGuid();

        var exitingTask = new TaskModel()
        {
            Id = taksId,
            User = user,
            UserId = user.Id,
            Title = "Titulo Original",
            Description = "Descrisção Original",
            Category = "ToDo",
            IsCompleted = false
        };
     
        var updatedDto = new TaskUpdateDto("Titulo Atualizado", "Descricao Atualizada", "InProgress");
        _taskRepoMock.Setup(x => x.GetByIdAsync(taksId)).ReturnsAsync(() => exitingTask);
        _taskRepoMock.Setup(x => x.Update(It.IsAny<TaskModel>()));
        _taskRepoMock.Setup(x => x.SaveChangesAsync());


        //Act

        var result = await _taskService.UpdateTaskAsync(updatedDto, taksId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(updatedDto.Title, result.Title);
        Assert.Equal(updatedDto.Description, result.Description);
        Assert.Equal(updatedDto.Category, result.Category);
        Assert.Equal(taksId, result.Id);
        Assert.False(_notifier.HasNotifications);
        _taskRepoMock.Verify(x => x.GetByIdAsync(taksId), Times.Once);
        _taskRepoMock.Verify(x => x.Update(It.IsAny<TaskModel>()), Times.Once);
        _taskRepoMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}

