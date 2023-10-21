using MediatR;
using Tasks.Query.Application.ModelsDto;

namespace Tasks.Query.Application.Queries.GetTask;

public class GetTaskQuery:IRequest<TaskDto>
{
    public Guid TaskId { get; set; }
}