using FluentValidation;

namespace RiverBooks.Books.BookEndpoints;

public class UpdateBookPriceRequestValidator : AbstractValidator<UpdateBookPriceRequest>
{
  public UpdateBookPriceRequestValidator()
  {
    RuleFor(x => x.Id).NotNull().NotEqual(Guid.Empty).WithMessage("A book id is required.");
    RuleFor(x => x.NewPrice).GreaterThan(0).WithMessage("New price must be greater than zero.");
  }
}
