using AutoMapper;
using Groups.Query.Application.ModelsDto;
using Groups.Query.Application.Queries.GetGroup;
using Groups.Query.Domain.Exceptions;
using Groups.Query.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Groups.Query.Application.Queries.GetGroupsCollection;

public class GetGroupsCollectionQueryHandler:IRequestHandler<GetGroupsCollectionQuery, List<GroupDto>>
{
    private readonly IGroupRepository _groupRepository;
    private readonly ILogger<GetGroupsCollectionQueryHandler> _logger;
    private readonly IMapper _map;

    public GetGroupsCollectionQueryHandler(IGroupRepository groupRepository, ILogger<GetGroupsCollectionQueryHandler> logger, IMapper map)
    {
        _groupRepository = groupRepository;
        _logger = logger;
        _map = map;
    }

    public async Task<List<GroupDto>> Handle(GetGroupsCollectionQuery request, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetGroupsByUserId(request.UserId);

        if (groups==null|| !groups.Any())
        {
            _logger.LogWarning($"groups for user: {request.UserId} not found!");
            throw new NotFoundException($"groups for user: {request.UserId} not found!");
        }

        return _map.Map<List<GroupDto>>(groups);
    }
}