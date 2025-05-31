using Microsoft.EntityFrameworkCore;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Interfaces;

namespace RiverBooks.OrderProcessing.Infrastructure.Data;

internal class EfOrderRepository : IOrderRepository
{
  private readonly OrderProcessingDbContext _context;
  public EfOrderRepository(OrderProcessingDbContext context)
  {
    _context = context;
  }

  public async Task AddAsync(Order order) => await _context.Orders.AddAsync(order);

  public async Task<List<Order>> ListAsync() => await _context.Orders
    .Include(o => o.OrderItems)
    .ToListAsync();

  public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
