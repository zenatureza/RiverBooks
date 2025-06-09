using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Infrastructure.Data;

internal class EfApplicationUserRepository : IApplicationUserRepository
{
  private readonly UsersDbContext _context;
  public EfApplicationUserRepository(UsersDbContext context)
  {
    _context = context;
  }

  public Task<ApplicationUser> GetUserByIdAsync(Guid userId)
  {
    return _context.ApplicationUsers
      .SingleAsync(user => user.Id == userId.ToString());
  }

  public async Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email)
  {
    return await _context.Users
      .Include(u => u.Addresses)
      .SingleAsync(u => u.Email == email);
  }

  public async Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
  {
    return await _context.Users
      .Include(u => u.CartItems)
      .SingleAsync(u => u.Email == email);
  }
  public async Task SaveChangesAsync()
  {
    await _context.SaveChangesAsync();
  }
}
