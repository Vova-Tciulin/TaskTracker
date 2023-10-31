using Groups.Query.Application.ModelsDto;
using MediatR;

namespace Groups.Query.Application.Queries.GetGroupsCollection;

public class GetGroupsCollectionQuery:IRequest<List<GroupDto>>
{
    public Guid UserId { get; set; }
}