using Groups.Cmd.Application.Models;

namespace Groups.Cmd.Application.Services;

public interface IIdentityService
{
    public Task<UserDto> GetUserByNickNameOrEmail(string name);
}