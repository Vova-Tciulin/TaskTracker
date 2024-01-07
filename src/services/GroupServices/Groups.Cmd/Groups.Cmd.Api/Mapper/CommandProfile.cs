using AutoMapper;
using Groups.Cmd.Api.Models;
using Groups.Cmd.Application.Commands.AddUser;
using Groups.Cmd.Application.Commands.CreateGroup;
using Groups.Cmd.Application.Commands.RemoveUser;

namespace Groups.Cmd.Api.Mapper;

public class CommandProfile:Profile
{
    public CommandProfile()
    {
        CreateMap<AddUserToGroup, AddUserCommand>();
        CreateMap<CreateGroup, CreateGroupCommand>();
        CreateMap<RemoveUserFromGroup, RemoveUserCommand>();
    }
}