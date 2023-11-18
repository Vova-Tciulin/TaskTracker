using AutoMapper;
using TaskTracker.Aggregators.Models;

namespace TaskTracker.Aggregators.Mapper;

public class MapProfile:Profile 
{
    public MapProfile()
    {
        CreateMap<TaskResponse, TaskModel>();

    }
}