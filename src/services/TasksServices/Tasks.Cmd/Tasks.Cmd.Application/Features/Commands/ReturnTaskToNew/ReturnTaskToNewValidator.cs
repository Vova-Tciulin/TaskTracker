using FluentValidation;

namespace Tasks.Cmd.Application.Features.Commands.ReturnTaskToNew;

public class ReturnTaskToNewValidator:AbstractValidator<ReturnTaskToNewCommand>
{
    public ReturnTaskToNewValidator()
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