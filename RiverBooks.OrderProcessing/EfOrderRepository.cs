using Microsoft.EntityFrameworkCore;

namespace RiverBooks.OrderProcessing;

internal class EfOrderRepository : IOrderRepository
{
  private readonly OrderProcessingDbContext _context;
  public EfOrderRepository(OrderProcessingDbContext context)
  {
    _context = context;
  }

  public async Task AddAsync(Order order) => await _context.Orders.AddAsync(order);

  public async Task<List<Order>> ListAsync() => await _context.Orders.ToListAsync();

  public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
