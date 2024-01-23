using MediatR;

namespace Cf.Dotnet.Architecture.Application.Commands;

public sealed record ReplaceOrderCommand(int OrderId) : IRequest;
