namespace RiverBooks.Users.CartEndpoints;

public record CartItemDto(Guid Id, Guid BookId, string Description, decimal Price, int Quantity);
