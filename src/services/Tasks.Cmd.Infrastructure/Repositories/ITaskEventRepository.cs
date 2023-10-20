using Tasks.Cmd.Domain.Models;

namespace Tasks.Cmd.Infrastructure.Contracts;
using System.Threading.Tasks;

public interface ITaskEventRepository
{
    Task SaveAsync(EventModel model);
    Task<List<EventModel>?> FindByAggregateId(Guid id);
}