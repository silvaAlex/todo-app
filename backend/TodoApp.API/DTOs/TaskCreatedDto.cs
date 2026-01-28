namespace TodoApp.API.DTOs;

public record TaskCreatedDto(string Title, string Description, string Category, Guid UserId);