using AutoMapper;
using Tasks.Cmd.Api.Models;
using Tasks.Cmd.Application.Features.Commands.CompleteTask;
using Tasks.Cmd.Application.Features.Commands.CreateTask;
using Tasks.Cmd.Application.Features.Commands.ExecuteTask;
using Tasks.Cmd.Application.Features.Commands.RemoveTask;
using Tasks.Cmd.Application.Features.Commands.UpdateTask;

namespace Tasks.Cmd.Api.Mapper;

public class CommandProfile:Profile
{
    public CommandProfile()
    {
        CreateMap<CompleteTaskDto, CompleteTaskCommand>();
        CreateMap<CreateTaskDto, CreateTaskCommand>();
        CreateMap<ExecuteTaskDto, ExecuteTaskCommand>();
        CreateMap<RemoveTaskDto, RemoveTaskCommand>();
        CreateMap<UpdateTaskDto, UpdateTaskCommand>();
    }
}