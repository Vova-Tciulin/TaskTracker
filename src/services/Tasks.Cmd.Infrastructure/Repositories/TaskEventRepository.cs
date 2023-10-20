using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Tasks.Cmd.Domain.Models;
using Tasks.Cmd.Infrastructure.Contracts;
using Tasks.Cmd.Infrastructure.Data;

namespace Tasks.Cmd.Infrastructure.Repositories;

public class TaskEventRepository:ITaskEventRepository
{
    private ITaskDbContext _db;
    private readonly ILogger<TaskEventRepository> _logger;

    public TaskEventRepository(ITaskDbContext db, ILogger<TaskEventRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task SaveAsync(EventModel model)
    {
        await _db.Events.InsertOneAsync(model);
    }

    public async Task<List<EventModel>?> FindByAggregateId(Guid id)
    {
        return await _db.Events.Find(u => u.AggregateId == id).ToListAsync();
        
    }
}