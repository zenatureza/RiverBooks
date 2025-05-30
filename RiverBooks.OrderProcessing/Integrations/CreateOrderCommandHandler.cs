using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.OrderProcessing.Contracts;

namespace RiverBooks.OrderProcessing.Integrations;

internal class CreateOrderCommandHandler(
  IOrderRepository orderRepository,
  ILogger<CreateOrderCommandHandler> logger) : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
{
  private readonly IOrderRepository _orderRepository = orderRepository;
  private readonly ILogger<CreateOrderCommandHandler> _logger = logger;

  public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
    var items = request.OrderItems.Select(oi => new OrderItem(
      oi.BookId,
      oi.Quantity,
      oi.UnitPrice,
      oi.Description));

    // TODO: find real addresses by IDs
    var shippingAddress = new Address("Coronel Blabla", "", "Santiago", "RS", "12345-678", "Brazil");
    var billingAddress = new Address("Coronel Blabla", "", "Santiago", "RS", "12345-678", "Brazil");

    var newOrder = Order.Factory.Create(
      request.UserId,
      shippingAddress,
      billingAddress,
      items);

    await _orderRepository.AddAsync(newOrder);
    await _orderRepository.SaveChangesAsync();

    _logger.LogInformation("Order {OrderId} created successfully for user {UserId}.", newOrder.Id, request.UserId);

    return new OrderDetailsResponse(newOrder.Id);
  }
}
