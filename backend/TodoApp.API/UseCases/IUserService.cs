using TodoApp.API.DTOs;
using TodoApp.API.Notifications;

namespace TodoApp.API.UseCases;

public interface IUserService
{
    Task<UserReadDto?> RegisterAsync(UserCreateDto user);
    Task<UserReadDto?> LoginAsync(UserLoginDto user);
    IReadOnlyCollection<Notification> Notifications { get; }
}

