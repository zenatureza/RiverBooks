using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.EmailSending.EmailBackgroundService;
using Serilog;

namespace RiverBooks.EmailSending;

public static class EmailSendingModuleServicesExtensions
{
  public static IServiceCollection AddEmailSendingModuleServices(
    this IServiceCollection services,
    IConfiguration config,
    ILogger logger,
    List<System.Reflection.Assembly> mediatRAssemblies)
  {
    //// configure MongoDB
    //services.Configure<MongoDBSettings>(config.GetSection("MongoDB"));
    //services.AddMongoDB(config);

    // Add module services
    services.AddTransient<ISendEmail, MimeKitEmailSender>();
    //services.AddTransient<IQueueEmailsInOutboxService, MongoDbQueueEmailOutboxService>();
    //services.AddTransient<IGetEmailsFromOutboxService, MongoDbGetEmailsFromOutboxService>();
    //services.AddTransient<ISendEmailsFromOutboxService,
    //  DefaultSendEmailsFromOutboxService>();

    //// if using MediatR in this module, add any assemblies that contain handlers to the list
    //mediatRAssemblies.Add(typeof(EmailSendingModuleServicesExtensions).Assembly);

    //// Add BackgroundWorker
    //services.AddHostedService<EmailSendingBackgroundService>();

    logger.Information("{Module} module services registered", "Email Sending");
    return services;
  }

}
