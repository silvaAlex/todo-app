using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Models;

namespace TodoApp.API.Data;

public class TodoAppDbContext(DbContextOptions<TodoAppDbContext> options) : DbContext(options)
{
    public DbSet<TaskModel> Tasks {  get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskModel>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId);

        base.OnModelCreating(modelBuilder);
    }
}

