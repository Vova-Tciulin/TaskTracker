using FluentValidation;

namespace Tasks.Cmd.Application.Features.Commands.CreateTask;

public class CreateTaskValidator:AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(u => u.Task)
            .NotEmpty().WithMessage("Описание задачи не может быть пустым!");
        
        RuleFor(u=>u.AuthorId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
        
        RuleFor(u=>u.GroupId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");

        RuleFor(u => u.DeadLine)
            .Must(BeAValidDateTime).WithMessage("Deadline не может быть меньше текущего времени ");
    }
    
    private bool BeAValidGuid(Guid guid)
    {
        return !guid.Equals(Guid.Empty);
    }

    private bool BeAValidDateTime(DateTime dateTime)
    {
        return dateTime > DateTime.Now;
    }
}