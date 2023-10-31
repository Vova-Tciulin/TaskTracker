using Groups.Query.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Groups.Query.Infrastructure.Data.Configurations;

public class GroupConfiguration:IEntityTypeConfiguration<GroupEntity>
{
    public void Configure(EntityTypeBuilder<GroupEntity> builder)
    {
        builder.ToTable("Group");
        builder.HasKey(u => u.Id);
        builder.HasMany(u => u.Tasks)
            .WithOne()
            .HasForeignKey(u => u.GroupId);
        builder.HasMany(u => u.Users)
            .WithOne()
            .HasForeignKey(u => u.GroupId);
    }
}