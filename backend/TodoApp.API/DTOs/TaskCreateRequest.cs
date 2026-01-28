namespace TodoApp.API.DTOs;

public record TaskCreateRequest(string Title, string Description, string Category);
