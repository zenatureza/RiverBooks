using Ardalis.Result;
using MediatR;
using RiverBooks.Books.Contracts;

namespace RiverBooks.Books.Integrations;

internal class BookDetailsQueryHandler(IBookService bookService) :
    IRequestHandler<BookDetailsQuery, Result<BookDetailsResponse>>
{
  private readonly IBookService _bookService = bookService;

  public async Task<Result<BookDetailsResponse>> Handle(BookDetailsQuery request, CancellationToken cancellationToken)
  {
    var book = await _bookService.GetBookByIdAsync(request.BookId);
    if (book is null)
    {
      return Result<BookDetailsResponse>.NotFound($"Book with ID {request.BookId} not found.");
    }

    var response = new BookDetailsResponse(book.Id, book.Title, book.Author, book.Price);
    return Result<BookDetailsResponse>.Success(response);
  }
}
