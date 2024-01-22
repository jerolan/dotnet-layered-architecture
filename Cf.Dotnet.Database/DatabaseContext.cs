using Cf.Dotnet.Architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cf.Dotnet.Database;

/// <summary>
/// Contexto de base de datos para la aplicación, utilizado para interactuar con la base de datos.
/// Hereda de DbContext, una clase de Entity Framework Core que facilita el mapeo entre objetos y registros de base de datos.
/// </summary>
public class DatabaseContext : DbContext
{
    /// <summary>
    /// Constructor sin parámetros para el contexto de base de datos.
    /// Utilizado por herramientas de Entity Framework Core.
    /// </summary>
    public DatabaseContext()
    {
    }

    /// <summary>
    /// Constructor que permite la configuración de opciones para el contexto de base de datos.
    /// </summary>
    /// <param name="options">Opciones de configuración para el contexto.</param>
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Buyer> Buyers { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var buyer = new Buyer(
            id: 1,
            name: "Test Buyer 1");
        var orderItem = new OrderItem(
            id: 1,
            productId: 1,
            productName: "Test Product 1",
            unitPrice: 10,
            units: 1);
        var order = new Order(
            id: 1,
            buyerId: 1);
        
        modelBuilder.Entity<Buyer>().HasData(buyer);
        modelBuilder.Entity<OrderItem>().HasData(orderItem);
        modelBuilder.Entity<Order>().HasData(order);
    }
}