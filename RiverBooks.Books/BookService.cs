﻿
namespace RiverBooks.Books;

internal class BookService(IBookRepository bookRepository) : IBookService
{
  private readonly IBookRepository _bookRepository = bookRepository;

  public async Task CreateBookAsync(BookDto newBook)
  {
    var book = new Book(newBook.Id, newBook.Title, newBook.Author, newBook.Price);
    await _bookRepository.AddAsync(book);
    await _bookRepository.SaveChangesAsync();
  }

  public async Task DeleteBookAsync(Guid id)
  {
    var bookToDelete = await _bookRepository.GetByIdAsync(id);
    if (bookToDelete is not null)
    {
      await _bookRepository.DeleteAsync(bookToDelete);
      await _bookRepository.SaveChangesAsync();
    }

    // TODO: Handle case where book is not found
  }

  public async Task<BookDto> GetBookByIdAsync(Guid id)
  {
    var book = await _bookRepository.GetByIdAsync(id);

    // TODO: Handle case where book is not found
    return new BookDto(book!.Id, book.Title, book.Author, book.Price);
  }

  public async Task<List<BookDto>> ListBooksAsync()
  {
    var books = (await _bookRepository.ListAsync())
      .Select(book => new BookDto(book.Id, book.Title, book.Author, book.Price));

    return [.. books];
  }

  public async Task UpdateBookPriceAsync(Guid bookId, decimal newPrice)
  {
    // validate the price

    var book = await _bookRepository.GetByIdAsync(bookId);

    // TODO: Handle case where book is not found

    book!.UpdatePrice(newPrice);
    await _bookRepository.SaveChangesAsync();
  }
}
