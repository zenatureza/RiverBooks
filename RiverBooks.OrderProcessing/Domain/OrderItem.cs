using Ardalis.GuardClauses;

namespace RiverBooks.OrderProcessing.Domain;

internal class OrderItem
{
  public OrderItem(Guid bookId, int quantity, decimal unitPrice, string description)
  {
    BookId = Guard.Against.Default(bookId);
    Quantity = Guard.Against.Negative(quantity);
    UnitPrice = Guard.Against.Negative(unitPrice);
    Description = Guard.Against.NullOrEmpty(description);
  }

  private OrderItem() { } // For EF Core

  public Guid Id { get; init; } = Guid.NewGuid();
  public Guid BookId { get; init; }
  public int Quantity { get; init; }
  public decimal UnitPrice { get; init; }
  public string Description { get; init; } = string.Empty;
}
