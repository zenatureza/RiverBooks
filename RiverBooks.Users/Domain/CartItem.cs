using Ardalis.GuardClauses;

namespace RiverBooks.Users.Domain;

public class CartItem
{
  public CartItem(Guid bookId, string description, decimal unitPrice, int quantity)
  {
    BookId = Guard.Against.Default(bookId);
    Description = Guard.Against.NullOrEmpty(description);
    UnitPrice = Guard.Against.Negative(unitPrice);
    Quantity = Guard.Against.Negative(quantity);
  }

  public CartItem()
  {
    // EF
  }

  public Guid Id { get; private set; } = Guid.NewGuid();
  public Guid BookId { get; private set; }
  public string Description { get; private set; } = string.Empty;
  public decimal UnitPrice { get; private set; }
  public int Quantity { get; private set; }

  internal void UpdateQuantity(int newQuantity) =>
    Quantity = Guard.Against.Negative(newQuantity);

  internal void UpdateDescription(string description) =>
    Description = Guard.Against.NullOrEmpty(description);

  internal void UpdatePrice(decimal unitPrice) =>
    UnitPrice = Guard.Against.Negative(unitPrice);
}
