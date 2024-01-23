using Cf.Dotnet.Architecture.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> logger;
    private readonly IDatabaseContext dbContext;

    public TransactionBehavior(
        IDatabaseContext dbContext,
        ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetType().Name;

        try
        {
            if (this.dbContext.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = this.dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                await using var transaction = await this.dbContext.BeginTransactionAsync();
                using (this.logger.BeginScope(new List<KeyValuePair<string, object>> { new("TransactionContext", transaction.TransactionId) }))
                {
                    this.logger.LogInformation("Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                    response = await next();

                    this.logger.LogInformation("Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                    await this.dbContext.CommitTransactionAsync();
                }
            });

            return response;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}