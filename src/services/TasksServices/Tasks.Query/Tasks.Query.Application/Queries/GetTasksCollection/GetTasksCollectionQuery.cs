using MediatR;
using Tasks.Query.Application.ModelsDto;

namespace Tasks.Query.Application.Queries.GetTasksCollection;

public class GetTasksCollectionQuery:IRequest<List<TaskDto>>
{
    public Guid GroupId { get; set; }
}