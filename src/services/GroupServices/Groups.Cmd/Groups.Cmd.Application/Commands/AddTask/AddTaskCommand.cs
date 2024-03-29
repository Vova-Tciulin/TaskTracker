﻿using MediatR;

namespace Groups.Cmd.Application.Commands.AddTask;

public class AddTaskCommand:IRequest<bool>
{
    public Guid GroupId { get; set; }
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
}