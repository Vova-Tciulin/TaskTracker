using MediatR;

namespace Tasks.Cmd.Application.Features.Commands.ReturnTaskToNew;

public class ReturnTaskToNewCommand:IRequest
{
    public Guid TaskId { get; set; }
    public Guid WorkerId { get; set; }
}