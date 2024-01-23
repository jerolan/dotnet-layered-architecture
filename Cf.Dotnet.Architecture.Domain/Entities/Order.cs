using Cf.Dotnet.Architecture.Domain.Enums;
using Cf.Dotnet.Architecture.Domain.Exceptions;
using Cf.Dotnet.Architecture.Domain.SeedWork;

namespace Cf.Dotnet.Architecture.Domain.Entities;

public class Order : IEntity
{
    private List<OrderItem> orderItems;

    public Order(int id, int buyerId)
    {
        Id = id;
        OrderStatus = OrderStatus.Created;
        BuyerId = buyerId;
    }

    public int BuyerId { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public IEnumerable<OrderItem> OrderItems => orderItems.AsReadOnly();
    public int Id { get; }

    public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, int units = 1)
    {
        var existingOrderForProduct = orderItems
            .SingleOrDefault(o => o.Id == productId);

        if (existingOrderForProduct is not null)
        {
            existingOrderForProduct.AddUnits(units);
        }
        else
        {
            var orderItem = new OrderItem(
                default,
                productId,
                productName,
                unitPrice,
                units
            );

            orderItems.Add(orderItem);
        }
    }

    public void Cancel()
    {
        if (OrderStatus == OrderStatus.Shipped)
            throw new OrderingDomainException($"Not possible to cancel order in {OrderStatus} status");

        OrderStatus = OrderStatus.Cancelled;
    }

    public void ReassignToBuyer(Buyer buyer)
    {
        BuyerId = buyer.Id;
    }

    public decimal GetTotal()
    {
        return orderItems.Sum(o => o.Units * o.UnitPrice);
    }

    public void Confirm()
    {
        if (OrderStatus is OrderStatus.Shipped or OrderStatus.Cancelled)
            throw new OrderingDomainException($"Not possible to cancel order in {OrderStatus} status");

        OrderStatus = OrderStatus.Confirmed;
    }
}