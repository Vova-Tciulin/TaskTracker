using AutoMapper;
using IdentityServer.Models;

namespace IdentityServer.Mapping;

public class AutoMap:Profile
{
    public AutoMap()
    {
        CreateMap<User, UserDto>();
    }
}