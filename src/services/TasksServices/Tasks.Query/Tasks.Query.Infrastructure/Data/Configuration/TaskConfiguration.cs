using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Query.Domain.Models;

namespace Tasks.Query.Infrastructure.Data.Configuration;


public class TaskConfiguration:IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("Tasks");
        builder.HasKey(u => u.TaskId);
        builder.Property(u => u.Task).IsRequired();
        builder.Property(u => u.TaskCreated).IsRequired();
        builder.Property(u => u.State).IsRequired();
        builder.Property(u => u.AuthorId).IsRequired();
        builder.Property(u => u.DeadLine).IsRequired();
        builder.Property(u => u.WorkerId).IsRequired(false);
        builder.Property(u => u.CompletedDateTime).IsRequired(false);

    }
}