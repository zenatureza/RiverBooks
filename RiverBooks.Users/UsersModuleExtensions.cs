﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace RiverBooks.Users;

public static class UsersModuleExtensions
{
  public static IServiceCollection AddUsersModuleServices(
    this IServiceCollection services,
    IConfiguration config,
    ILogger logger)
  {
    string? connectionString = config.GetConnectionString("UsersConnectionString");
    services.AddDbContext<UsersDbContext>(options =>
      options.UseSqlServer(connectionString));

    services.AddIdentityCore<ApplicationUser>()
      .AddEntityFrameworkStores<UsersDbContext>();

    logger.Information("{Module} module services registered", "Users");

    return services;
  }
}
