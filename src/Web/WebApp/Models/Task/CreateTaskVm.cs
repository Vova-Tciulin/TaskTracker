﻿namespace WebApp.Models.Task;

public class CreateTaskVm
{
    public Guid GroupId { get; set; }
    public string Title { get; set; }
    public string Task { get; set; }

    public string DeadLine { get; set; }
    

    public CreateTaskVm()
    {
        
    }
}