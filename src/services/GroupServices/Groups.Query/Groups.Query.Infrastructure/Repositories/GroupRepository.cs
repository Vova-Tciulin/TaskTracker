using Groups.Query.Domain.Entities;
using Groups.Query.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Groups.Query.Infrastructure.Repositories;

public class GroupRepository:IGroupRepository
{
    private readonly GroupDbContext _db;

    public GroupRepository(GroupDbContext db)
    {
        _db = db;
    }

    public async Task<GroupEntity?> GetByIdAsync(Guid groupId)
    {
        return await _db.Groups
            .Include(u => u.Tasks)
            .Include(u => u.Users)
            .FirstOrDefaultAsync(u => u.Id == groupId);
    }

    public async Task<List<GroupEntity>?> GetGroupsByUserId(Guid userId)
    {
        return await _db.Groups
            .Include(u => u.Users)
            .Include(u => u.Tasks)
            .Where(u=>u.Users.Any(user=>user.UserId==userId))
            .ToListAsync();
    }

    public void RemoveGroup(GroupEntity group)
    {
        _db.Groups.Remove(group);
    }

    public void UpdateGroup(GroupEntity group)
    {
        _db.Groups.Update(group);
    }

    public void CreateGroup(GroupEntity group)
    {
        _db.Groups.Add(group);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}