namespace RiverBooks.OrderProcessing.Contracts;

public record class OrderItemDetails(
  Guid BookId,
  int Quantity,
  decimal UnitPrice,
  string Description);
