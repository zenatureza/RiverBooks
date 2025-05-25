namespace RiverBooks.Books.BookEndpoints;

public record CreateBookRequest(Guid? Id, string Title, string Author, decimal Price);
