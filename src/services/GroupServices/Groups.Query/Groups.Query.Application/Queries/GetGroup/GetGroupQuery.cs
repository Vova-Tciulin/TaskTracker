using Groups.Query.Application.ModelsDto;
using MediatR;

namespace Groups.Query.Application.Queries.GetGroup;

public class GetGroupQuery:IRequest<GroupDto>
{
    public Guid GroupId { get; set; }
}