using Cf.Dotnet.Architecture.Domain.SeedWork;
using Cf.DotnetArchitecture.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Cf.Dotnet.Database;

public sealed class Repository<T> : IRepository<T> where T : class, IEntity
{
    private readonly DbSet<T> set;

    public Repository(DatabaseContext context, IUnitOfWork unitOfWork)
    {
        set = context.Set<T>();
        UnitOfWork = unitOfWork;
    }

    public IUnitOfWork UnitOfWork { get; }

    public void Add(T entity)
    {
        set.Add(entity);
    }

    public void Update(T entity)
    {
        set.Update(entity);
    }

    public void Remove(T entity)
    {
        set.Remove(entity);
    }

    public Task<T> FindAsync(int id)
    {
        return set.Where(x => x.Id == id).FirstAsync();
    }

    public Task<List<T>> ToListAsync()
    {
        return set.ToListAsync();
    }
}