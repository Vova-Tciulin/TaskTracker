using WebApp.Services.ModelDto.Group;

namespace WebApp.Services.Group;

public interface IGroupService
{
    Task<GroupDto> CreateGroup(CreateGroupDto model);
    Task AddUserToGroup(AddUserToGroupDto model);
    Task RemoveGroup(Guid groupId);
    Task RemoveUserFromGroup(RemoveUserFromGroupDto model);
    
    Task<GroupAggregatorDto> GetGroupAggregatorById(string groupId);
    Task<List<GroupDto>> GetGroupsByUserId(string userId);
    
}