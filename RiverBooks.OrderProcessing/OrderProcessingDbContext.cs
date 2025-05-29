using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.OrderProcessing;

internal class OrderProcessingDbContext : DbContext
{
  public DbSet<Order> Orders { get; set; }

  public OrderProcessingDbContext(DbContextOptions<OrderProcessingDbContext> options) : base(options)
  {
  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema("OrderProcessing");

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(modelBuilder);
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>()
      .HavePrecision(18, 6);
  }
}
