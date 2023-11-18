﻿namespace WebApp.Services.ModelDto.Group;

public class GroupDto
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Description { get; set; }
    public List<UserDto> Users { get; set; }
    
}