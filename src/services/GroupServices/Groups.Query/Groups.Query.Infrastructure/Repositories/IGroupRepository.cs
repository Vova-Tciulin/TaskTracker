using Groups.Query.Domain.Entities;

namespace Groups.Query.Infrastructure.Repositories;

public interface IGroupRepository
{
    Task<GroupEntity?> GetByIdAsync(Guid groupId);
    Task<List<GroupEntity>?> GetGroupsByUserId(Guid userId);
    void RemoveGroup(GroupEntity group);
    void UpdateGroup(GroupEntity group);
    void CreateGroup(GroupEntity group);
    Task SaveChangesAsync();
}