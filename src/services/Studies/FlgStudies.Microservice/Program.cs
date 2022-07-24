using Example.Cqrs;
using Example.EfCore.Extensions;
using Example.EventBus.Abstractions;
using Example.EventBus.Kafka.Extensions;
using Example.IntegrationEvents.EmiasFlg;
using Example.IntegrationEvents.FlgMl;
using Example.IntegrationEvents.FlgSeries;
using Example.IntegrationEvents.FlgValidations;
using Example.Logging.Serilog.Extensions;
using Example.Mapper;
using FlgStudies.Microservice.Persistence;

var builder = WebApplication.CreateBuilder(args);
var host = builder.Host;
var configuration = builder.Configuration;
var services = builder.Services;
host.UseLoggingSerilog("celsus", "FlgStudies.Microservice", configuration["ELASTICSEARCH_HOST"]);

services.AddInternalKafkaEventBus(configuration);
services.AddAppAutoMapper();
services.AddAppCqrs<AppDbContext>();
services.AddAppDbContext<AppDbContext>(configuration["DEFAULT_CONNECTION"]);

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Services.MigrateAppDbContext<AppDbContext>();

var eventBus = app.Services.GetService<IEventBus>()!;
eventBus.Subscribe<FlgStudyReceivedIntegrationEvent>();
eventBus.Subscribe<FlgStudyValidateSuccessIntegrationEvent>();
eventBus.Subscribe<FlgStudyValidateFailedIntegrationEvent>();
eventBus.Subscribe<FlgStudyMlResearchedIntegrationEvent>();
eventBus.Subscribe<FlgStudyDefineSeriesFailedIntegrationEvent>();
eventBus.Subscribe<FlgStudyDefineSeriesSuccessIntegrationEvent>();

app.Run();
