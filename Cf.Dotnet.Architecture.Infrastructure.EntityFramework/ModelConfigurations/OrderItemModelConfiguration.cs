using Cf.Dotnet.Architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cf.Dotnet.Database.ModelConfigurations;

public class OrderItemModelConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        var orderItem = new OrderItem(
            1,
            1,
            "Test Product 1",
            10,
            1);

        builder.HasData(orderItem);

        builder.Property<byte[]>("Version")
            .IsRowVersion();
    }
}