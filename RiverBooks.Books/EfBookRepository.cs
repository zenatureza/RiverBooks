using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

internal class EfBookRepository(BookDbContext dbContext) : IBookRepository
{
  private readonly BookDbContext _dbContext = dbContext;

  public async Task<Book?> GetByIdAsync(Guid id)
  {
    return await _dbContext.Books.FindAsync(id);
  }
  public async Task<List<Book>> ListAsync()
  {
    return await _dbContext.Books.ToListAsync();
  }
  public async Task AddAsync(Book book)
  {
    await _dbContext.Books.AddAsync(book);
  }
  public Task DeleteAsync(Book book)
  {
    _dbContext.Books.Remove(book);

    return Task.CompletedTask;
  }
  public async Task SaveChangesAsync()
  {
    await _dbContext.SaveChangesAsync();
  }
}
