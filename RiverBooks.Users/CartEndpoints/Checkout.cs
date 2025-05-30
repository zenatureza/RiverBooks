using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.Cart.Checkout;

internal class Checkout(IMediator mediator) : Endpoint<CheckoutRequest, CheckoutResponse>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Post("/cart/checkout");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(CheckoutRequest req, CancellationToken ct)
  {
    var emailAddress = User.FindFirstValue("EmailAddress");

    var command = new CheckoutCartCommand(
      emailAddress!,
      req.ShippingAddressId,
      req.BillingAddressId);

    var result = await _mediator.Send(command, ct);
    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
    }
    else
    {
      var response = new CheckoutResponse(result.Value);
      await SendOkAsync(response, ct);
    }
  }
}
