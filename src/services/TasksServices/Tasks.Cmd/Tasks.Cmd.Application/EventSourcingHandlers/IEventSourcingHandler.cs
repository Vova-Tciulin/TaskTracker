using Tasks.Cmd.Domain.Aggregates;

namespace Tasks.Cmd.Application.EventSourcingHandlers;

public interface IEventSourcingHandler<T>
{
    Task SaveAsync(AggregateRoot aggregate);
    Task<T> GetByIdAsync(Guid aggregateId);
}