using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Users.Data;
using Serilog;

namespace RiverBooks.Users;

public static class UsersModuleServiceExtensions
{
  public static IServiceCollection AddUsersModuleServices(
    this IServiceCollection services,
    IConfiguration config,
    ILogger logger,
    List<System.Reflection.Assembly> mediatRAssemblies)
  {
    string? connectionString = config.GetConnectionString("UsersConnectionString");
    services.AddDbContext<UsersDbContext>(options =>
      options.UseSqlServer(connectionString));

    services.AddIdentityCore<ApplicationUser>()
      .AddEntityFrameworkStores<UsersDbContext>();

    services.AddScoped<IApplicationUserRepository, EfApplicationUserRepository>();
    services.AddScoped<IReadOnlyUserStreetAddressRepository, EfUserStreetAddressRepository>();

    // Add MediatR Domain Event Dispatcher
    services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

    mediatRAssemblies.Add(typeof(UsersModuleServiceExtensions).Assembly);

    logger.Information("{Module} module services registered", "Users");

    return services;
  }
}
