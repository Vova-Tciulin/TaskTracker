﻿using AutoMapper;
using WebApp.Models.Groups;
using WebApp.Models.Task;
using WebApp.Models.User;
using WebApp.Services.ModelDto.Group;
using WebApp.Services.ModelDto.Task;

namespace WebApp.Mapper;

public class MapProfile:Profile
{
    public MapProfile()
    {
        CreateMap<GroupDto, GroupVm>();
        CreateMap<TaskDto, TaskVm>()
            .ForMember(dest=>dest.State, opt=>opt.MapFrom(src=>
                src.State==0?"New":src.State==1?"InWork":"Finished"));;
        CreateMap<GroupAggregatorDto, GroupAggregatorVm>();
        CreateMap<TaskDto, UpdateTaskVm>()
            .ForMember(dest=>dest.DeadLine,opt=>opt.MapFrom(src=>src.DeadLine.ToString("s")));
        CreateMap<UserDto, UserVm>();
    }
}