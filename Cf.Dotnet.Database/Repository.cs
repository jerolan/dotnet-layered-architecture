using Cf.Dotnet.Architecture.Domain.SeedWork;

namespace Cf.Dotnet.Database;

public abstract class Repository<T> : IRepository<T> where T : IEntity, IQueryable
{
    public IUnitOfWork UnitOfWork { get; }
}