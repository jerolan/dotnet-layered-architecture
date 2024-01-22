using Cf.Dotnet.Architecture.Domain.Enums;
using Cf.Dotnet.Architecture.Domain.Exceptions;
using Cf.Dotnet.Architecture.Domain.SeedWork;

namespace Cf.Dotnet.Architecture.Domain.Entities;

public class Order : IEntity
{
    public Order(int id, int buyerId)
    {
        this.Id = id;
        this.OrderStatus = OrderStatus.Created;
        this.BuyerId = buyerId;
    }
    public int Id { get; private set; }

    public int BuyerId { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public IReadOnlyCollection<OrderItem> OrderItems => orderItems;
    private readonly List<OrderItem> orderItems = new ();

    public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, int units = 1)
    {
        var existingOrderForProduct = this.orderItems
            .SingleOrDefault(o => o.Id == productId);

        if (existingOrderForProduct is not null)
        {
            existingOrderForProduct.AddUnits(units);
        }
        else
        {
            var orderItem = new OrderItem(
                id: default,
                productId: productId,
                productName: productName,
                unitPrice: unitPrice,
                units: units
            );
            
            this.orderItems.Add(orderItem);
        }
    }
    
    public void Cancel()
    {
        if (OrderStatus == OrderStatus.Shipped)
        {
            throw new OrderingDomainException($"Not possible to cancel order in {OrderStatus} status");
        }

        OrderStatus = OrderStatus.Cancelled;
    }
}