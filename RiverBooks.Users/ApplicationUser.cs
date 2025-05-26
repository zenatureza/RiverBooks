using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users;

public class ApplicationUser : IdentityUser
{
  public string FullName { get; set; } = string.Empty;

  private readonly List<CartItem> _cartItems = [];
  public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

  public void AddItemToCart(CartItem item)
  {
    Guard.Against.Null(item, nameof(item));
    var existingBook = _cartItems.SingleOrDefault(c => c.BookId == item.BookId);
    if (existingBook != null)
    {
      existingBook.UpdateQuantity(existingBook.Quantity + item.Quantity);
      return;
    }

    _cartItems.Add(item);
  }
}

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
}
