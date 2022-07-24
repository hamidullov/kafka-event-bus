using Example.Cqrs;
using Example.EfCore.Extensions;
using Example.EventBus.Abstractions;
using Example.EventBus.Kafka.Extensions;
using Example.IntegrationEvents.FlgStudies;
using Example.Logging.Serilog.Extensions;
using Example.Mapper;
using FlgValidations.Microservice.Persistence;

var builder = WebApplication.CreateBuilder(args);
var host = builder.Host;
var configuration = builder.Configuration;
var services = builder.Services;
host.UseLoggingSerilog("celsus", "FlgValidations.Microservice", configuration["ELASTICSEARCH_HOST"]);

services.AddInternalKafkaEventBus(configuration);
services.AddAppAutoMapper();
services.AddAppCqrs<AppDbContext>();
services.AddAppDbContext<AppDbContext>(configuration["DEFAULT_CONNECTION"]);

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Services.MigrateAppDbContext<AppDbContext>();

var eventBus = app.Services.GetService<IEventBus>()!;
eventBus.Subscribe<FlgStudyPlacedIntegrationEvent>();
eventBus.Subscribe<FlgStudyStartedIntegrationEvent>();

app.Run();