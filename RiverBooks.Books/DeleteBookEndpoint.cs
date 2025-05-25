using FastEndpoints;

namespace RiverBooks.Books;

internal class DeleteBookEndpoint(IBookService bookService) :
    Endpoint<DeleteBookRequest>()
{
  private readonly IBookService _bookService = bookService;

  public override void Configure()
  {
    Delete("/books/{Id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(DeleteBookRequest req, CancellationToken ct)
  {
    // TODO: handle not found
    await _bookService.DeleteBookAsync(req.Id);
    
    await SendNoContentAsync(ct);
  }
}
