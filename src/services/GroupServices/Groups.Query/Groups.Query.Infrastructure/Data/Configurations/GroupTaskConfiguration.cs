using Groups.Query.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Groups.Query.Infrastructure.Data.Configurations;

public class GroupTaskConfiguration:IEntityTypeConfiguration<GroupTask>
{
    public void Configure(EntityTypeBuilder<GroupTask> builder)
    {
        builder.ToTable("GroupTask");
        builder.HasKey(u => new { u.GroupId, u.TaskId });
    }
}