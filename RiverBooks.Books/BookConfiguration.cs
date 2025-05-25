using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.Books;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
  internal static readonly Guid Book1Guid = Guid.Parse("5bf6c1ac-97ea-4c31-8920-f8849822c340");
  internal static readonly Guid Book2Guid = Guid.Parse("9873e1bb-433c-4431-9b08-0035695d3291");
  internal static readonly Guid Book3Guid = Guid.Parse("c6436b74-ae4e-45e2-86e0-ce1a84d77b6f");

  public void Configure(EntityTypeBuilder<Book> builder)
  {
    builder.Property(x => x.Title)
      .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
      .IsRequired();

    builder.Property(x => x.Author)
      .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
      .IsRequired();

    builder.HasData(GetSampleBookData());
  }

  private static IEnumerable<Book> GetSampleBookData()
  {
    var tolkien = "J.R.R. Tolkien";

    yield return new Book(Book1Guid, "The Fellowship of the Ring", tolkien, 10.99m);
    yield return new Book(Book2Guid, "The Two Towers", tolkien, 11.99m);
    yield return new Book(Book3Guid, "The Return of the King", tolkien, 12.99m);
  }
}
