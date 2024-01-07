using Groups.Cmd.Domain.Models;
using MongoDB.Driver;

namespace Groups.Cmd.Infrastructure.Data;

public interface IGroupDbContext
{
    IMongoCollection<EventModel> Events { get; }
}