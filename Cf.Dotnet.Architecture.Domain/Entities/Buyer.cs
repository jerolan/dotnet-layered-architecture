using Cf.Dotnet.Architecture.Domain.SeedWork;

namespace Cf.Dotnet.Architecture.Domain.Entities;

public class Buyer : IEntity
{
    public Buyer(int id)
    {
    }

    public Buyer(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; private set; }
    public decimal Balance { get; private set; }

    public int Id { get; }

    public void UpdateBalance(decimal o)
    {
        Balance -= o;
    }
}