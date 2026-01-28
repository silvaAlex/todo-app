using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Models;
using Task = TodoApp.Api.Models.Task;

namespace TodoApp.API.Data;

public class TodoAppDbContext(DbContextOptions<TodoAppDbContext> options) : DbContext(options)
{
    public DbSet<Task> Tasks {  get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId);

        base.OnModelCreating(modelBuilder);
    }
}

