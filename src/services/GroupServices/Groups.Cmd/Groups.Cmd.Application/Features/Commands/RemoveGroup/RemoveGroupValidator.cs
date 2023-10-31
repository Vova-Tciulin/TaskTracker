using FluentValidation;

namespace Groups.Cmd.Application.Features.Commands.RemoveGroup;

public class RemoveGroupValidator:AbstractValidator<RemoveGroupCommand>
{
    public RemoveGroupValidator()
    {
        RuleFor(u=>u.UserId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
        
        RuleFor(u=>u.GroupId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
    }
    private bool BeAValidGuid(Guid guid)
    {
        return !guid.Equals(Guid.Empty);
    }
}