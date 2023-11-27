using WebApp.Services.ModelDto.Group;

namespace WebApp.Services.Moq;

public class GroupServiceMoq:IGroupService
{
    private readonly Db _db;

    public GroupServiceMoq(Db db)
    {
        _db = db;
    }

    public Task<GroupDto> CreateGroup(CreateGroupDto model)
    {
        throw new NotImplementedException();
    }

    public Task AddUserToGroup(AddUserToGroupDto model)
    {
        throw new NotImplementedException();
    }

    public Task RemoveGroup(Guid groupId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUserFromGroup(RemoveUserFromGroupDto model)
    {
        throw new NotImplementedException();
    }

    public async Task<GroupAggregatorDto> GetGroupAggregatorById(string groupId)
    {
        return await Task.FromResult(_db.GroupAggregator);
    }

    public async Task<List<GroupDto>> GetGroupsByUserId(string userId)
    {
        return await Task.FromResult(_db.Groups);
    }
}