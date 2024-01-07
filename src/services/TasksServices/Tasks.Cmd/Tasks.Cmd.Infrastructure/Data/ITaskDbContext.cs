using MongoDB.Driver;
using Tasks.Cmd.Domain.Models;
using Tasks.Common.Events;

namespace Tasks.Cmd.Infrastructure.Data;

public interface ITaskDbContext
{
    IMongoCollection<EventModel> Events { get; }
}