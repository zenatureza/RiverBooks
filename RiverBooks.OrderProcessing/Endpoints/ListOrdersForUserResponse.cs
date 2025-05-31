using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Interfaces;

namespace RiverBooks.OrderProcessing.Endpoints;

internal class ListOrdersForUserResponse
{
  public List<OrderSummary> Orders { get; set; } = [];
}

internal class ListOrdersForUserQueryHandler(IOrderRepository orderRepository) :
  IRequestHandler<ListOrdersForUserQuery, Result<List<OrderSummary>>>
{
  public async Task<Result<List<OrderSummary>>> Handle(ListOrdersForUserQuery request, CancellationToken cancellationToken)
  {
    // TODO: filter by user email?
    var orders = await orderRepository.ListAsync();

    var summaries = orders.Select(x => new OrderSummary
    {
      DateCreated = x.DateCreated,
      OrderId = x.Id,
      UserId = x.UserId,
      Total = x.OrderItems.Sum(oi => oi.UnitPrice) // need to .Include OrderItems
    }).ToList();

    return summaries;
  }
}
