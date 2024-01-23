using Cf.Dotnet.Architecture.Domain.Entities;
using Cf.Dotnet.Architecture.Domain.SeedWork;
using Cf.Dotnet.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

// Configurando el logging para que utilice la consola.
builder.Logging.AddConsole();

// Configurando los servicios de la aplicación.
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddUnitOfWork();

// Construyendo el host de la aplicación.
using var host = builder.Build();

var orderRepository = host.Services.GetRequiredService<IRepository<Order>>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();

var id = Convert.ToInt32(Environment.GetCommandLineArgs()[1]);
var order = await orderRepository.FindAsync(id);
order.Cancel();

await orderRepository.UnitOfWork.SaveChangesAsync();

logger.LogInformation("Order {OrderId} cancelled", order.Id);