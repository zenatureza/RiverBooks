using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.UseCases;

public record AddItemToCartCommand(Guid BookId, int Quantity, string EmailAddress) : IRequest<Result>;

public class AddItemToCartCommandHandler(IApplicationUserRepository userRepository) : 
  IRequestHandler<AddItemToCartCommand, Result>
{
  public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
  {
    var user = await userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

    if (user is null) return Result.Unauthorized();

    // TODO: get description and price from books module
    var newCartItem = new CartItem(request.BookId, "description", 1.00m, request.Quantity);

    user.AddItemToCart(newCartItem);
    await userRepository.SaveChangesAsync();

    return Result.Success();
  }
}
