using Cf.Dotnet.Architecture.Domain.SeedWork;

namespace Cf.Dotnet.Architecture.Domain.Entities;

public class Buyer : IEntity
{
    public Buyer(int id)
    {
    }
    
    public Buyer(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
    
    public int Id { get; private set; }

    public string Name { get; private set; }
    public decimal Balance { get; private set; }

    public void UpdateBalance(decimal o)
        => this.Balance -= o;
}