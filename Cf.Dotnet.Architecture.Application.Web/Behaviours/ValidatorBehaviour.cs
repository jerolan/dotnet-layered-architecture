using Cf.Dotnet.Architecture.Domain.Exceptions;
using FluentValidation;
using MediatR;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> logger;
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
    {
        this.validators = validators;
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var typeName = request.GetType().Name;

        logger.LogInformation("Validating command {CommandType}", typeName);

        var failures = validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}",
                typeName, request, failures);

            throw new OrderingDomainException(
                $"Command Validation Errors for type {typeof(TRequest).Name}",
                new ValidationException("Validation exception", failures));
        }

        return await next();
    }
}