namespace Cf.Dotnet.Architecture.Domain.SeedWork;

public interface IRepository<T> where T : IEntity, IQueryable
{
    IUnitOfWork UnitOfWork { get; }
}