using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Tasks.Cmd.Application.Behaviours;

public class ValidationBehaviour<TRequest,TResponse>:IPipelineBehavior<TRequest, TResponse>
    where TRequest: IRequest<TResponse>
{
    private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
    {
        _logger = logger;
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await 
                Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                _logger.LogError($"Validation {request.GetType()} failed. Failures: {failures}");
                throw new ValidationException(failures);
            }
        }

        return await next();
    }
}