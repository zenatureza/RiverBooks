using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;

namespace RiverBooks.OrderProcessing.Endpoints;

internal class ListOrdersForUser(IMediator mediator) : EndpointWithoutRequest<ListOrdersForUserResponse>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Get("/orders");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var emailAddress = User.FindFirstValue("EmailAddress");

    var query = new ListOrdersForUserQuery(emailAddress!);

    var result = await _mediator.Send(query, ct);

    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
    }
    else
    {
      var response = new ListOrdersForUserResponse
      {
        Orders = [.. result.Value.Select(x => new OrderSummary
        {
          DateCreated = x.DateCreated,
          DateShipped = x.DateShipped,
          OrderId = x.OrderId,
          Total = x.Total,
          UserId = x.UserId,
        })]
      };
      await SendAsync(response, cancellation: ct);
    }
  }
}
