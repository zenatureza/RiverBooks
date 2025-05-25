﻿using FastEndpoints;
using RiverBooks.Books;
using RiverBooks.Users;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting up the application...");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => 
  config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFastEndpoints();

builder.Services.AddBookServices(builder.Configuration, logger);
builder.Services.AddUsersModuleServices(builder.Configuration, logger);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseFastEndpoints();

app.Run();

public partial class Program { } // for testing purposes
