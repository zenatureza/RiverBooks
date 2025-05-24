using FastEndpoints;

namespace RiverBooks.Books;

// https://fast-endpoints.com
internal class ListBooksEndpoint(IBookService bookService) : 
    EndpointWithoutRequest<ListBooksResponse>()
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct) => 
        await SendAsync(new ListBooksResponse
        {
            Books = _bookService.ListBooks()
        }, cancellation: ct);
}