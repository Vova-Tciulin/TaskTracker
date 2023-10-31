using AutoMapper;
using Group.Common.Events;
using Groups.Query.Application.ModelsDto;
using Groups.Query.Domain.Entities;

namespace Groups.Query.Application.Mapping;

public class AutoMapProfile:Profile
{
    public AutoMapProfile()
    {
        CreateMap<GroupCreatedEvent, GroupEntity>();
        CreateMap<GroupEntity, GroupDto>();
        CreateMap<GroupUser, UserDto>();
        CreateMap<GroupTask, TaskDto>();
    }
}