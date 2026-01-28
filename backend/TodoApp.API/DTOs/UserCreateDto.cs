namespace TodoApp.API.DTOs;

public record UserCreateDto()
{
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

