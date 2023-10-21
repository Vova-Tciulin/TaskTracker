using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tasks.Query.Application.ModelsDto;
using Tasks.Query.Domain.Exceptions;
using Tasks.Query.Infrastructure.Repositories;

namespace Tasks.Query.Application.Queries.GetTask;

public class GetTaskQueryHandler:IRequestHandler<GetTaskQuery, TaskDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<GetTaskQueryHandler> _logger;
    private readonly IMapper _map;

    public GetTaskQueryHandler(ITaskRepository taskRepository, ILogger<GetTaskQueryHandler> logger, IMapper map)
    {
        _taskRepository = taskRepository;
        _logger = logger;
        _map = map;
    }

    public async Task<TaskDto> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId);
        if (task==null)
        {
            _logger.LogError($"Task with Id: {request.TaskId} not found!");
            throw new NotFoundException($"Task with Id: {request.TaskId} not found!");
        }
        
        return _map.Map<TaskDto>(task);
    }
}