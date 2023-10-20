using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Tasks.Cmd.Domain.Models;
using Tasks.Common.Events;

namespace Tasks.Cmd.Infrastructure.Data;

public class TaskDbContext:ITaskDbContext
{
    public IMongoCollection<EventModel> Events { get; }

    public TaskDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Events = database.GetCollection<EventModel>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
    }
}