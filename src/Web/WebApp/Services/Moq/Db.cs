using System.Runtime.InteropServices.JavaScript;
using WebApp.Services.ModelDto.Group;
using WebApp.Services.ModelDto.Task;

namespace WebApp.Services.Moq;

public class Db
{
    public List<TaskDto> Tasks { get; set; }
    public List<GroupDto> Groups { get; set; }
    public List<UserDto> Users { get; set; }
    public GroupAggregatorDto GroupAggregator { get; set; }

    public Db()
    {
        Users = new List<UserDto>()
        {
            new UserDto()
            {
                Id = Guid.NewGuid(),
                NickName = "Qrauzeer"
            },
            new UserDto()
            {
                Id = Guid.NewGuid(),
                NickName = "Alex"
            }
        };
        
        Tasks = new List<TaskDto>()
        {
            new TaskDto()
            {
                TaskId = Guid.NewGuid(),
                AuthorId = Users[0].Id,
                Title = "задача 1",
                Task = "тестовое описание задачи №3",
                TaskCreated = DateTime.Now,
                State = 0,
                DeadLine = DateTime.Now,
                GroupId = Guid.NewGuid(),
                User = Users[0]
            },
            new TaskDto()
            {
                TaskId = Guid.NewGuid(),
                AuthorId = Users[0].Id,
                Title = "задача 2",
                Task = "тестовое описание задачи №2",
                State = 0,
                TaskCreated = DateTime.Parse("2022-12-21 12:10:50"),
                DeadLine = DateTime.Parse("2023-10-21 12:10:50"),
                GroupId = Guid.NewGuid(),
                User = Users[0]
            },
            new TaskDto()
            {
                TaskId = Guid.NewGuid(),
                AuthorId = Users[1].Id,
                State = 0,
                Title = "задача 3",
                Task = "тестовое описание задачи №3",
                TaskCreated = DateTime.Now,
                DeadLine = DateTime.Parse("2023-12-21 12:10:50"),
                GroupId = Guid.NewGuid(),
                User = Users[0]
            },
            new TaskDto()
            {
                TaskId = Guid.NewGuid(),
                AuthorId = Users[1].Id,
                Title = "задача 4",
                State = 1,
                Task = "тестовое описание задачи №4",
                TaskCreated = DateTime.Now,
                DeadLine = DateTime.Parse("2023-12-21 12:10:50"),
                GroupId = Guid.NewGuid(),
                WorkerId = Users[0].Id,
                User = Users[0]
            },
            new TaskDto()
            {
                TaskId = Guid.NewGuid(),
                AuthorId = Users[0].Id,
                Title = "задача 5",
                State = 2,
                Task = "тестовое описание задачи №4",
                TaskCreated = DateTime.Now,
                DeadLine = DateTime.Parse("2023-12-21 12:10:50"),
                GroupId = Guid.NewGuid(),
                WorkerId = Users[1].Id,
                CompletedDateTime = DateTime.Parse("2023-12-22 12:10:50"),
                User = Users[0]
            },
            new TaskDto()
            {
                TaskId = Guid.NewGuid(),
                AuthorId = Users[0].Id,
                Title = "задача 6",
                State = 2,
                Task = "тестовое описание задачи №4",
                TaskCreated = DateTime.Now,
                DeadLine = DateTime.Parse("2023-12-21 12:10:50"),
                GroupId = Guid.NewGuid(),
                WorkerId = Users[0].Id,
                CompletedDateTime = DateTime.Parse("2023-11-21 12:10:50"),
                User = Users[0]
            },
        };
        
        Groups = new List<GroupDto>()
        {
            new GroupDto()
            {
                AuthorId = Users[0].Id,
                Description = "Группа 1",
                Id = Guid.NewGuid(),
                Users = Users
            },
            new GroupDto()
            {
                AuthorId = Users[1].Id,
                Description = "Группа 2",
                Id = Guid.NewGuid(),
                Users = Users
            },
        };
        GroupAggregator = new GroupAggregatorDto()
        {
            AuthorId = Users[0].Id,
            Id = Groups[0].Id,
            Description = "Группа 1",
            Tasks = Tasks,
            Users = Users
        };
    }
}