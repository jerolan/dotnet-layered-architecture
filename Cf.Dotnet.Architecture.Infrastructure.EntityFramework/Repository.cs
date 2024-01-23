using Cf.Dotnet.Architecture.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Cf.Dotnet.Database;

public sealed class Repository<T> : IRepository<T> where T : class, IEntity
{
    private readonly DatabaseContext context;
    
    public IUnitOfWork UnitOfWork { get; }

    public Repository(DatabaseContext context, IUnitOfWork unitOfWork)
    {
        this.context = context;
        this.UnitOfWork = unitOfWork;
    }

    public void Add(T entity)
        => this.context.Set<T>().Add(entity);

    public void Update(T entity)
        => this.context.Set<T>().Update(entity);

    public void Remove(T entity)
        => this.context.Set<T>().Remove(entity);

    public Task<T> FindAsync(int id)
        => this.context.Set<T>().Where(x => x.Id == id).FirstAsync();

    public Task<List<T>> ToListAsync()
        => this.context.Set<T>().ToListAsync();
}