using Cf.Dotnet.Architecture.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cf.Dotnet.Architecture.Infrastructure.EntityFramework.MediatR.Behaviours;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IDatabaseContext dbContext;
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> logger;

    public TransactionBehavior(
        IDatabaseContext dbContext,
        ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetType().Name;

        try
        {
            if (dbContext.HasActiveTransaction) return await next();

            var strategy = dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await dbContext.BeginTransactionAsync();
                using (logger.BeginScope(new List<KeyValuePair<string, object>>
                           { new("TransactionContext", transaction.TransactionId) }))
                {
                    logger.LogInformation("Begin transaction {TransactionId} for {CommandName} ({@Command})",
                        transaction.TransactionId, typeName, request);

                    response = await next();

                    logger.LogInformation("Commit transaction {TransactionId} for {CommandName}",
                        transaction.TransactionId, typeName);

                    await dbContext.CommitTransactionAsync();
                }
            });

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}