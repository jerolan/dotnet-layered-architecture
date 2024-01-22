using Cf.Dotnet.Architecture.Application.Models;
using MediatR;

namespace Cf.Dotnet.Architecture.Application.Queries;

public sealed record GetOrdersSummaryQuery : IRequest<List<OrdersSummary>>;
