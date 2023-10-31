using Groups.Cmd.Domain.Aggregates;

namespace Groups.Cmd.Application.EventSourcingHandlers;

public interface IEventSourcingHandler<T> 
{
    Task SaveAsync(AggregateRoot aggregate);
    Task<T> GetByIdAsync(Guid aggregateId);
}