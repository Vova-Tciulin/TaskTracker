using TaskTracker.Aggregators.Models;

namespace TaskTracker.Aggregators.Services;

public interface IGroupService
{
    Task<GroupResponse> GetGroupById(Guid groupId);
}