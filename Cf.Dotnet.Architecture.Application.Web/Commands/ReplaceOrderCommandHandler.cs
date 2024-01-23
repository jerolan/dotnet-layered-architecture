using Cf.Dotnet.Architecture.Domain.Entities;
using Cf.Dotnet.Architecture.Domain.SeedWork;
using MediatR;

namespace Cf.Dotnet.Architecture.Application.Commands;

internal sealed class ReplaceOrderCommandHandler : IRequestHandler<ReplaceOrderCommand>
{
    private readonly IRepository<Order> orderRepository;

    public ReplaceOrderCommandHandler(IRepository<Order> orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task Handle(ReplaceOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = await this.orderRepository.FindAsync(request.OrderId);
        order.Cancel();
        
        var replacementOrder = new Order(request.OrderId, order.BuyerId);
        this.orderRepository.Add(replacementOrder);
    }
}