using Groups.Query.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Groups.Query.Infrastructure.Data.Configurations;

public class GroupUserConfiguration:IEntityTypeConfiguration<GroupUser>
{
    public void Configure(EntityTypeBuilder<GroupUser> builder)
    {
        builder.ToTable("GroupUser");
        builder.HasKey(u => new { u.GroupId, u.UserId });
        
    }
}