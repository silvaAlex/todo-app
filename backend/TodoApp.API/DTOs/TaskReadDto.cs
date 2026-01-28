namespace TodoApp.API.DTOs;


public record TaskReadDto(Guid Id, Guid UserId, string Title, string Description, string Category, bool IsCompleted, DateTime CreatedAt, DateTime UpdatedAt);

