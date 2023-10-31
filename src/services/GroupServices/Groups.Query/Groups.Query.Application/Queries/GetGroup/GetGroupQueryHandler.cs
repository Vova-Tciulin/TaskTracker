using AutoMapper;
using Groups.Query.Application.ModelsDto;
using Groups.Query.Domain.Exceptions;
using Groups.Query.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Groups.Query.Application.Queries.GetGroup;

public class GetGroupQueryHandler:IRequestHandler<GetGroupQuery, GroupDto>
{
    private readonly IGroupRepository _groupRepository;
    private readonly ILogger<GetGroupQueryHandler> _logger;
    private readonly IMapper _map;

    public GetGroupQueryHandler(IGroupRepository groupRepository, ILogger<GetGroupQueryHandler> logger, IMapper map)
    {
        _groupRepository = groupRepository;
        _logger = logger;
        _map = map;
    }

    public async Task<GroupDto> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.GroupId);

        if (group==null)
        {
            _logger.LogWarning($"group with id: {request.GroupId} not found!");
            throw new NotFoundException($"group with id: {request.GroupId} not found!");
        }

        return _map.Map<GroupDto>(group);
    }
}