using Tasks.Cmd.Application.Models;

namespace Tasks.Cmd.Application.Services;

public interface IGroupService
{
    Task<GroupModel> GetGroupById(Guid groupId);
}