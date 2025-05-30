﻿using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases;
using RiverBooks.Users.UseCases.Cart.AddItem;

namespace RiverBooks.Users.CartEndpoints;

internal class AddItem(IMediator mediator) : Endpoint<AddCartItemRequest>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Post("/cart");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(AddCartItemRequest req, CancellationToken ct)
  {
    var emailAddress = User.FindFirstValue("EmailAddress");

    var command = new AddItemToCartCommand(req.BookId, req.Quantity, emailAddress!);

    var result = await _mediator.Send(command, ct);

    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
    }
    else
    {
      await SendOkAsync(ct);
    }
  }
}
