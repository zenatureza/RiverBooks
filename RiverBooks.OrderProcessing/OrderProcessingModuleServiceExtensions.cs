using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.OrderProcessing.Integrations;
using Serilog;

namespace RiverBooks.OrderProcessing;

public static class OrderProcessingModuleServiceExtensions
{
  public static IServiceCollection AddOrderProcessingModuleServices(
    this IServiceCollection services,
    IConfiguration config,
    ILogger logger,
    List<System.Reflection.Assembly> mediatRAssemblies)
  {
    var connectionString = config.GetConnectionString("OrderProcessingConnectionString");
    services.AddDbContext<OrderProcessingDbContext>(options =>
      options.UseSqlServer(connectionString));

    services.AddScoped<IOrderRepository, EfOrderRepository>();
    services.AddScoped<IOrderAddressCache, RedisOrderAddressCache>();

    mediatRAssemblies.Add(typeof(OrderProcessingModuleServiceExtensions).Assembly);

    logger.Information("{Module} module services registered", "OrderProcessing");

    return services;
  }
}
