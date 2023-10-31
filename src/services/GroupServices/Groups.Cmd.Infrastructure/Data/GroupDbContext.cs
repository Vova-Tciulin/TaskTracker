using Groups.Cmd.Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Groups.Cmd.Infrastructure.Data;

public class GroupDbContext:IGroupDbContext
{
    public IMongoCollection<EventModel> Events { get; }
    
    public GroupDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Events = database.GetCollection<EventModel>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
    }
}