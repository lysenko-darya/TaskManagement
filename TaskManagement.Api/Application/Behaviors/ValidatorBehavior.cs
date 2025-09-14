using FluentValidation;
using MediatR;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Api.Application.Behaviors;

public class ValidatorBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidatorBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators
        ?? throw new ArgumentNullException(nameof(validators));

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Count != 0)
        {
            _logger.LogWarning("Validation errors - Command: {@Command} - Errors: {@ValidationErrors}", request, failures);

            throw new DomainException(
                $"Command Validation Errors for type {typeof(TRequest).Name}", new ValidationException("Validation exception", failures));
        }

        return await next(cancellationToken);
    }
}