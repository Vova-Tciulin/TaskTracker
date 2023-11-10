using AutoMapper;
using TaskTracker.Aggregators.Models;

namespace TaskTracker.Aggregators.Mapper;

public class MapProfile:Profile 
{
    public MapProfile()
    {
        CreateMap<TaskResponse, TaskModel>()
            .ForMember(dest=>dest.State, opt=>opt.MapFrom(src=>
                src.State==0?"New":src.State==1?"InWork":"Finished"));
    }
}