using TaskTracker.Aggregators.Models;

namespace TaskTracker.Aggregators.Services.Group;

public interface IGroupService
{
    Task<GroupResponse> GetGroupById(Guid groupId);
}