using FluentValidation;

namespace Tasks.Cmd.Application.Features.Commands.UpdateTask;

public class UpdateTaskValidator:AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskValidator()
    {
        RuleFor(u=>u.TaskId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
        
        RuleFor(u=>u.AuthorId)
            .NotEmpty().WithMessage("Guid не может быть пустым")
            .Must(BeAValidGuid).WithMessage("Недопустимый формат Guid");
        
        
        RuleFor(model => model)
            .Must(HaveAtLeastOnePropertyNotNull)
            .WithMessage("Хотя бы одно из полей не должно быть пустым.");
    }
    
    private bool BeAValidGuid(Guid guid)
    {
        return !guid.Equals(Guid.Empty);
    }
    
    private bool BeAValidDateTime(DateTime? dateTime)
    {
        if (!dateTime.HasValue)
        {
            return false;
        }
        
        return dateTime.Value > DateTime.Now;
    }

    private bool HaveAtLeastOnePropertyNotNull(UpdateTaskCommand model)
    {
        return model.NewTask != null || model.NewDeadLine != null|| model.NewTitle !=null;
    }
}