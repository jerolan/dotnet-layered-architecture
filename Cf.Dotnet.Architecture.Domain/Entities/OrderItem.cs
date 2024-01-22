using Cf.Dotnet.Architecture.Domain.Exceptions;
using Cf.Dotnet.Architecture.Domain.SeedWork;
using Cf.Dotnet.Architecture.Domain.Specifications;

namespace Cf.Dotnet.Architecture.Domain.Entities;

public class OrderItem : IEntity
{
    public OrderItem(int id, int productId, string productName, decimal unitPrice, int units = 1)
    {
        if (new UnitsSpecification().IsNotSatisfiedBy(units))
        {
            throw new OrderingDomainException($"Invalid number of units: {units}");
        }

        Id = id;
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Units = units;
    }

    public int Id { get; private set; }
    
    public int Units { get; private set; }

    public decimal UnitPrice { get; set; }

    public string ProductName { get; set; }

    public int ProductId { get; set; }

    public void AddUnits(int nextUnits)
    {
        if (new UnitsSpecification().IsNotSatisfiedBy(nextUnits))
        {
            throw new OrderingDomainException($"Invalid number of units: {nextUnits}");
        }

        this.Units += nextUnits;
    }
}