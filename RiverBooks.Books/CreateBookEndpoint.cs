﻿using FastEndpoints;

namespace RiverBooks.Books;

internal class CreateBookEndpoint(IBookService bookService) :
    Endpoint<CreateBookRequest, BookDto>()
{
  private readonly IBookService _bookService = bookService;

  public override void Configure()
  {
    Post("/books");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateBookRequest req, CancellationToken ct)
  {
    var bookDto = new BookDto(
      req.Id ?? Guid.NewGuid(),
      req.Title,
      req.Author,
      req.Price);
    await _bookService.CreateBookAsync(bookDto);
    
    await SendCreatedAtAsync<GetBookByIdEndpoint>(new { bookDto.Id }, bookDto, cancellation: ct);
  }
}
