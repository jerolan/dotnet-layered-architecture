using Cf.Dotnet.Architecture.Domain.Entities;
using Cf.Dotnet.Architecture.Domain.SeedWork;
using MediatR;

namespace Cf.Dotnet.Architecture.Application.Services;

internal sealed class ConfirmOrderServiceHandler : IRequestHandler<ConfirmOrderService>
{
    private readonly IRepository<Order> orderRepository;
    private readonly IRepository<Buyer> buyerRepository;

    public ConfirmOrderServiceHandler(IRepository<Order> orderRepository, IRepository<Buyer> buyerRepository)
    {
        this.orderRepository = orderRepository;
        this.buyerRepository = buyerRepository;
    }

    public async Task Handle(ConfirmOrderService request, CancellationToken cancellationToken)
    {
        Order order = await this.orderRepository.FindAsync(request.OrderId);
        Buyer buyer = await this.buyerRepository.FindAsync(order.BuyerId);

        buyer.UpdateBalance(order.GetTotal());
        this.buyerRepository.Update(buyer);

        order.Confirm();
        this.orderRepository.Update(order);
        
        await this.orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}