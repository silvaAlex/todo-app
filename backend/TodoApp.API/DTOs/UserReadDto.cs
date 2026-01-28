namespace TodoApp.API.DTOs;

public record UserReadDto(Guid Id, string UserName, DateTime CreatedAt);
