using FastEndpoints;
using RiverBooks.Books;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints();

builder.Services.AddBookServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseFastEndpoints();

app.Run();
