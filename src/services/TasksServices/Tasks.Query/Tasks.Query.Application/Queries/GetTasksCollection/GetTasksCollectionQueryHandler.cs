using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tasks.Query.Application.ModelsDto;
using Tasks.Query.Application.Queries.GetTask;
using Tasks.Query.Domain.Exceptions;
using Tasks.Query.Infrastructure.Repositories;

namespace Tasks.Query.Application.Queries.GetTasksCollection;

public class GetTasksCollectionQueryHandler:IRequestHandler<GetTasksCollectionQuery, List<TaskDto>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<GetTaskQueryHandler> _logger;
    private readonly IMapper _map;

    public GetTasksCollectionQueryHandler(IMapper map, ITaskRepository taskRepository, ILogger<GetTaskQueryHandler> logger)
    {
        _map = map;
        _taskRepository = taskRepository;
        _logger = logger;
    }

    public async Task<List<TaskDto>> Handle(GetTasksCollectionQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetTasksByGroupIdAsync(request.GroupId);

        if (tasks==null||!tasks.Any())
        {
            _logger.LogError($"Tasks with group Id: {request.GroupId} not found");
            throw new NotFoundException($"Tasks with group Id: {request.GroupId} not found");
        }

        return _map.Map<List<TaskDto>>(tasks);
    }
}