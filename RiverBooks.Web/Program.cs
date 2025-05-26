using System.Reflection;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
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

builder.Services.AddFastEndpoints()
  .AddJWTBearerAuth(builder.Configuration["Auth:JwtSecret"]!)
  .AddAuthorization()
  .SwaggerDocument();

List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddBookServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUsersModuleServices(builder.Configuration, logger, mediatRAssemblies);

builder.Services.AddMediatR(cfg =>
  cfg.RegisterServicesFromAssemblies([.. mediatRAssemblies]));

var app = builder.Build();

app.UseAuthentication()
  .UseAuthorization();

app.UseFastEndpoints()
  .UseSwaggerGen();

app.Run();

public partial class Program { } // for testing purposes
