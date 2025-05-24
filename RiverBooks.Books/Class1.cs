using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Books;

internal interface IBookService
{
    IEnumerable<BookDto> ListBooks();
}

public record BookDto(Guid Id, string Title, string Author);

internal class BookService : IBookService
{
    private readonly List<BookDto> _books =
    [
        new BookDto(Guid.NewGuid(), "The Great Gatsby", "F. Scott Fitzgerald"),
        new BookDto(Guid.NewGuid(), "To Kill a Mockingbird", "Harper Lee"),
        new BookDto(Guid.NewGuid(), "1984", "George Orwell")
    ];
    public IEnumerable<BookDto> ListBooks()
    {
        return _books;
    }
}

public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        app.MapGet("/books", (IBookService bookService) =>
        {
            return bookService.ListBooks();
        });
    }
}

public static class BookServiceExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services)
    {
        services.AddSingleton<IBookService, BookService>();

        return services;
    }
}