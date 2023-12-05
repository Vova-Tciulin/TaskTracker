using TaskTracker.Aggregators.Models;

namespace TaskTracker.Aggregators.Services.Identity;

public interface IIdentityService
{
    Task<UserDto> GetUserInfoById(Guid userId);
}