using FastEndpoints;

namespace RiverBooks.OrderProcessing.Endpoints;

internal class ListOrdersForUser : EndpointWithoutRequest<ListOrdersForUserResponse>
{
  public override void Configure()
  {
    Get("/orders");
    Claims("EmailAddress");
  }
}

internal class ListOrdersForUserResponse
{
  public List<OrderSummary> Orders { get; set; } = [];
}

public class OrderSummary
{
  public Guid UserId { get; set; }
  public DateTimeOffset DateCreated { get; set; }
  public DateTimeOffset? DateShipped { get; set; }
  public decimal Total { get; set; }
}
