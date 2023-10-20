﻿namespace Tasks.Cmd.Api.Models;

public class UpdateTaskDto
{
    public Guid TaskId { get; set; }
    public Guid AuthorId { get; set; }
    public string? NewTask { get; set; }
    public DateTime? NewDeadLine { get; set; }
}