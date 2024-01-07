using FluentValidation;

namespace Groups.Cmd.Application.Commands.CreateGroup;

public class CreateGroupValidator:AbstractValidator<CreateGroupCommand>
{
    public CreateGroupValidator()
    {
        RuleFor(u=>u.UserId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
        RuleFor(u => u.Description)
            .NotEmpty()
            .NotNull();
    }
    private bool BeAValidGuid(Guid guid)
    {
        return !guid.Equals(Guid.Empty);
    }
}