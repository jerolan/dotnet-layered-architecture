using Cf.Dotnet.Architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cf.Dotnet.Database.ModelConfigurations;

public class OrderModelConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        var order = new Order(
            id: 1,
            buyerId: 1);

        builder.HasData(order);
        
        builder.Property<byte[]>("Version")
            .IsRowVersion();
    }
}