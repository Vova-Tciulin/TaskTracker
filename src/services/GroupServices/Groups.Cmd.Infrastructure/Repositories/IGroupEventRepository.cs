using Groups.Cmd.Domain.Models;

namespace Groups.Cmd.Infrastructure.Repositories;

public interface IGroupEventRepository
{
    Task SaveAsync(EventModel model);
    Task<List<EventModel>?> FindByAggregateId(Guid id);
}