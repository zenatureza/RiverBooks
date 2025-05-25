using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Books.BookEndpoints;
using Xunit.Abstractions;

namespace RiverBooks.Books.Tests.Endpoints;

public class BookGetById(Fixture fixture, ITestOutputHelper outputHelper) :
  TestClass<Fixture>(fixture, outputHelper)
{
  [Theory]
  [InlineData("5bf6c1ac-97ea-4c31-8920-f8849822c340", "The Fellowship of the Ring")]
  [InlineData("9873e1bb-433c-4431-9b08-0035695d3291", "The Two Towers")]
  [InlineData("c6436b74-ae4e-45e2-86e0-ce1a84d77b6f", "The Return of the King")]
  public async Task ReturnsExpectedBookGivenIdAsync(string validId, string expectedTitle)
  {
    Guid id = Guid.Parse(validId);
    var request = new GetBookByIdRequest(id);
    var testResult = await
      Fixture.Client.GETAsync<GetById, GetBookByIdRequest, BookDto>(request);

    testResult.Response.EnsureSuccessStatusCode();
    testResult.Result.Title.Should().Be(expectedTitle);
  }
}

