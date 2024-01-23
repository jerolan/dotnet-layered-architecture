using Cf.Dotnet.Architecture.Application.Models;
using Cf.Dotnet.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cf.Dotnet.Architecture.Application.Queries;

internal sealed class GetOrdersSummaryQueryHandler : IRequestHandler<GetOrdersSummaryQuery, List<OrdersSummary>>
{
    private readonly DatabaseContext _context;

    public GetOrdersSummaryQueryHandler(DatabaseContext context)
    {
        this._context = context;
    }

    public async Task<List<OrdersSummary>> Handle(GetOrdersSummaryQuery request, CancellationToken cancellationToken)
    {
       List<OrdersSummary> ordersSummaries = await this._context.Database.SqlQueryRaw<OrdersSummary>(
           sql: """
              select
                  o.Id as Id,
                  o.OrderStatus as Status,
                  SUM(oi.UnitPrice * oi.Units) as Total,
                  b.Name as BuyerName
              from Orders o
              join OrderItems oi on o.Id = oi.OrderId
              join Buyers b on o.BuyerId = b.Id
            """)
           .ToListAsync(cancellationToken);

       return ordersSummaries;
    }
}