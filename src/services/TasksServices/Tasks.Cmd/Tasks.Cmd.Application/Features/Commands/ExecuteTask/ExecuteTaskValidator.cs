using FluentValidation;

namespace Tasks.Cmd.Application.Features.Commands.ExecuteTask;

public class ExecuteTaskValidator:AbstractValidator<ExecuteTaskCommand>
{
    public ExecuteTaskValidator()
    {
        RuleFor(u=>u.TaskId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
        
        RuleFor(u=>u.WorkerId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
    }
    
    private bool BeAValidGuid(Guid guid)
    {
        return !guid.Equals(Guid.Empty);
    }
}