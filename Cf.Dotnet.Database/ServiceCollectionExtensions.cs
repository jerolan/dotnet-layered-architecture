using Cf.Dotnet.Architecture.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cf.Dotnet.Database;

public static class ServiceCollectionExtensions
{
    private const string ConnectionString = "ConnectionString";
    
    public static IServiceCollection AddDatabase(this IServiceCollection services, string sectionName = "ConnectionString")
    {
       services
            .AddDbContext<DatabaseContext>(config =>
        {
            config.UseSqlite(ConnectionString);
        });

        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
    
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        // services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}