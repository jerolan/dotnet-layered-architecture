using Cf.Dotnet.Architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cf.Dotnet.Database.ModelConfigurations;

public class OrderItemModelConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        var orderItem = new OrderItem(
            id: 1,
            productId: 1,
            productName: "Test Product 1",
            unitPrice: 10,
            units: 1);
        
        builder.HasData(orderItem);
        
        builder.Property<byte[]>("Version")
            .IsRowVersion();
    }
}