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
        RuleFor(u=>u.UserId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
    }
    
    private bool BeAValidGuid(Guid guid)
    {
        return !guid.Equals(Guid.Empty);
    }
}