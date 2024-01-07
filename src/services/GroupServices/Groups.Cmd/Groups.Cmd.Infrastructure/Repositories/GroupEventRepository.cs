using System.Text.Json;
using Groups.Cmd.Domain.Models;
using Groups.Cmd.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Groups.Cmd.Infrastructure.Repositories;

public class GroupEventRepository:IGroupEventRepository
{
    private readonly IGroupDbContext _db;
    private readonly ILogger<GroupEventRepository> _logger;

    public GroupEventRepository(IGroupDbContext db, ILogger<GroupEventRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task SaveAsync(EventModel model)
    {
        _logger.LogInformation($"save model in db. Model: {JsonSerializer.Serialize(model)}");
        await _db.Events.InsertOneAsync(model);
    }

    public async Task<List<EventModel>?> FindByAggregateId(Guid id)
    {
        return await _db.Events.Find(u => u.AggregateId == id).ToListAsync();
    }
}