﻿namespace Tasks.Cmd.Api.Models;

public class UpdateTaskDto
{
    public Guid TaskId { get; set; }
    public string? NewTask { get; set; }
    public string? NewTitle { get; set; }
    public DateTime? NewDeadLine { get; set; }
}