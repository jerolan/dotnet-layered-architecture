namespace Cf.Dotnet.Architecture.Domain.SeedWork;

public interface IRepository<T> where T : IEntity
{
    public IUnitOfWork UnitOfWork { get; }

    void Add(T entity);

    void Update(T entity);

    void Remove(T entity);

    Task<T> FindAsync(int id);

    Task<List<T>> ToListAsync();
}