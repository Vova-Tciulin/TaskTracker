﻿namespace Tasks.Cmd.Api.Models;

public class CreateTaskDto
{
    public Guid GroupId { get; set; }
    public string Task { get; set; }
    public DateTime DeadLine { get; set; }
}