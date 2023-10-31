using AutoMapper;
using Groups.Cmd.Api.Models;
using Groups.Cmd.Application.Features.Commands.AddUser;
using Groups.Cmd.Application.Features.Commands.CreateGroup;
using Groups.Cmd.Application.Features.Commands.RemoveGroup;
using Groups.Cmd.Application.Features.Commands.RemoveUser;

namespace Groups.Cmd.Api.Mapper;

public class CommandProfile:Profile
{
    public CommandProfile()
    {
        CreateMap<AddUserToGroup, AddUserCommand>();
        CreateMap<CreateGroup, CreateGroupCommand>();
        CreateMap<RemoveGroup, RemoveGroupCommand>();
        CreateMap<RemoveUserFromGroup, RemoveUserCommand>();
    }
}