using Groups.Query.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Groups.Query.Infrastructure.Data;

public class GroupDbContext:DbContext
{
    public DbSet<GroupEntity> Groups { get; set; }
    public DbSet<GroupTask> GroupTasks { get; set; }
    public DbSet<GroupUser> GroupUsers { get; set; }

    public GroupDbContext(DbContextOptions<GroupDbContext> options)
    :base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GroupDbContext).Assembly);
    }
}