using Microsoft.EntityFrameworkCore;
using Tasks.Query.Domain.Models;

namespace Tasks.Query.Infrastructure.Data;

public class TaskDbContext:DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }

    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        :base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskDbContext).Assembly);
    }
}