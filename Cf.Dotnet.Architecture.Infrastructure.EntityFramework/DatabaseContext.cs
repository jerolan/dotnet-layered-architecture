using System.Data;
using Cf.Dotnet.Architecture.Domain.Entities;
using Cf.Dotnet.Architecture.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cf.Dotnet.Database;

/// <summary>
/// Contexto de base de datos para la aplicación, utilizado para interactuar con la base de datos.
/// Hereda de DbContext, una clase de Entity Framework Core que facilita el mapeo entre objetos y registros de base de datos.
/// </summary>
public class DatabaseContext : DbContext, IUnitOfWork
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

    public bool HasActiveTransaction => this.currentTransaction != null;
    private IDbContextTransaction? currentTransaction;

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

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        this.currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        return this.currentTransaction;
    }

    public async Task CommitTransactionAsync()
    {
        if (this.currentTransaction is null)
        {
            throw new InvalidOperationException("No active transaction");
        }

        try
        {
            await SaveChangesAsync();
            await this.currentTransaction.CommitAsync();
        }
        catch
        {
            this.RollbackTransaction();
            throw;
        }
        finally
        {
            this.currentTransaction.Dispose();
            this.currentTransaction = null;
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            this.currentTransaction?.Rollback();
        }
        finally
        {
            if (this.currentTransaction is not null)
            {
                this.currentTransaction.Dispose();
                this.currentTransaction = null;
            }
        }
    }
}