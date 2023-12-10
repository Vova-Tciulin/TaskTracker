using FluentValidation;

namespace Groups.Cmd.Application.Features.Commands.AddUser;

public class AddUserValidator:AbstractValidator<AddUserCommand>
{
    public AddUserValidator()
    {
        RuleFor(u=>u.GroupId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
        RuleFor(u=>u.AuthorId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
        RuleFor(u=>u.NickNameOrEmail)
            .NotEmpty().NotNull().WithMessage("NickName или Email не может быть пустым!");
    }
    
    private bool BeAValidGuid(Guid guid)
    {
        return !guid.Equals(Guid.Empty);
    }
}