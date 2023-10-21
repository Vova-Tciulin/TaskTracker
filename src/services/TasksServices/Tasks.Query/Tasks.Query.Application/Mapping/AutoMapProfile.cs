using AutoMapper;
using Tasks.Common.Events;
using Tasks.Query.Application.ModelsDto;
using Tasks.Query.Domain.Models;

namespace Tasks.Query.Application.Mapping;

public class AutoMapProfile:Profile
{
    public AutoMapProfile()
    {
        CreateMap<TaskCreatedEvent, TaskEntity>()
            .ForMember(dest=>dest.TaskId, opt=>opt.MapFrom(src=>src.Id));
        CreateMap<TaskEntity, TaskDto>();
    }
}