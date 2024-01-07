using FluentValidation;

namespace Groups.Cmd.Application.Commands.RemoveUser;

public class RemoveUserValidator: AbstractValidator<RemoveUserCommand>
{
    public RemoveUserValidator()
    {
        
        RuleFor(u=>u.GroupId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
        RuleFor(u=>u.UserId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
    }
    private bool BeAValidGuid(Guid guid)
    {
        return !guid.Equals(Guid.Empty);
    }
}