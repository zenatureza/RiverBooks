using Ardalis.Result;
using MediatR;
using RiverBooks.Books.Contracts;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.Cart.AddItem;

public class AddItemToCartCommandHandler(IApplicationUserRepository userRepository, IMediator mediator) : 
  IRequestHandler<AddItemToCartCommand, Result>
{
  public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
  {
    var user = await userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

    if (user is null) return Result.Unauthorized();
    
    var bookResponse = await mediator.Send(new BookDetailsQuery(request.BookId), cancellationToken);
    if (bookResponse.Status == ResultStatus.NotFound)
      return Result.NotFound($"Book with ID {request.BookId} not found.");
    
    var newCartItem = new CartItem(request.BookId, 
      $"{bookResponse.Value.Title} by {bookResponse.Value.Author}",
      bookResponse.Value.Price,
      request.Quantity);

    user.AddItemToCart(newCartItem);
    await userRepository.SaveChangesAsync();
    
    return Result.Success();
  }
}
