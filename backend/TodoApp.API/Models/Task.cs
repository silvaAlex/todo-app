namespace TodoApp.Api.Models;

public class Task
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category {  get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime? CreatedDate { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;



}