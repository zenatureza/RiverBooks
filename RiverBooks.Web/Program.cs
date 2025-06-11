using System.Reflection;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using RiverBooks.Books;
using RiverBooks.OrderProcessing;
using RiverBooks.SharedKernel;
using RiverBooks.Users;
using RiverBooks.EmailSending;
using RiverBooks.Users.UseCases.Cart.AddItem;
using RiverBooks.Reporting;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting up the application...");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => 
  config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddFastEndpoints()
  .AddJWTBearerAuth(builder.Configuration["Auth:JwtSecret"]!)
  .AddAuthorization()
  .SwaggerDocument();

List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddBookModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUsersModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddOrderProcessingModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddEmailSendingModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddReportingModuleServices(builder.Configuration, logger, mediatRAssemblies);

builder.Services.AddMediatR(cfg =>
  cfg.RegisterServicesFromAssemblies([.. mediatRAssemblies]));
builder.Services.AddMediatRLoggingBehavior();
builder.Services.AddMediatRFluentValidationBehavior();
builder.Services.AddValidatorsFromAssemblyContaining<AddItemToCartCommandValidator>();
builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

var app = builder.Build();

app.UseAuthentication()
  .UseAuthorization();

app.UseFastEndpoints()
  .UseSwaggerGen();

app.Run();

public partial class Program { } // for testing purposes
