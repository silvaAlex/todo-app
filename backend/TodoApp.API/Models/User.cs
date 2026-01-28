namespace TodoApp.Api.Models;
using System.Collections.Generic;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; } 
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;


    public ICollection<Task> Tasks { get; set; } = [];
}