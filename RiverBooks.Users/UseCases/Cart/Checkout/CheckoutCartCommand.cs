using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Contracts;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.Cart.Checkout;

public record CheckoutCartCommand(string EmailAddress, Guid ShippingAddressId, Guid BillingAddressId) :
  IRequest<Result<Guid>>;

public class CheckoutCartCommandHandler(
  IApplicationUserRepository userRepository,
  IMediator mediator) : IRequestHandler<CheckoutCartCommand, Ardalis.Result.Result<Guid>>
{
  private readonly IApplicationUserRepository _userRepository = userRepository;
  private readonly IMediator _mediator = mediator;

  public async Task<Result<Guid>> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
    if (user is null) return Result.Unauthorized();

    var items = user.CartItems.Select(ci => new OrderItemDetails(
      ci.BookId, 
      ci.Quantity,
      ci.UnitPrice,
      ci.Description)).ToList();

    var createOrderCommand = new CreateOrderCommand(
      Guid.Parse(user.Id),
      request.ShippingAddressId,
      request.BillingAddressId,
      items);

    var result = await _mediator.Send(createOrderCommand, cancellationToken);
    if (!result.IsSuccess) return result.Map(x => x.OrderId);

    user.ClearCart();
    await _userRepository.SaveChangesAsync();

    return Result.Success(result.Value.OrderId);
  }
}
