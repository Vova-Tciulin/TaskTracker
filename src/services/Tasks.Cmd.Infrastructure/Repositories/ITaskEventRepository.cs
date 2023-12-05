using Tasks.Cmd.Domain.Models;

namespace Tasks.Cmd.Infrastructure.Repositories;

public interface ITaskEventRepository
{
    Task SaveAsync(EventModel model);
    Task<List<EventModel>?> FindByAggregateId(Guid id);
    
}