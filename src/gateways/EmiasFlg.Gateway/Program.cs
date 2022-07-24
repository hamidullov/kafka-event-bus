using EmiasFlgGateway.Domain;
using EmiasFlgGateway.Persistence;
using Example.Cqrs;
using Example.EfCore;
using Example.EfCore.Extensions;
using Example.EventBus.Abstractions;
using Example.EventBus.Kafka.Extensions;
using Example.IntegrationEvents.EmiasFlg;
using Example.IntegrationEvents.FlgStudies;
using Example.Logging.Serilog.Extensions;
using Example.Mapper;

var builder = WebApplication.CreateBuilder(args);
var host = builder.Host;
var configuration = builder.Configuration;
var services = builder.Services;
host.UseLoggingSerilog("celsus", "EmiasFlg.Gateway", configuration["ELASTICSEARCH_HOST"]);

services.AddInternalKafkaEventBus(configuration);
services.AddAppAutoMapper();
services.AddAppCqrs<AppDbContext>();
services.AddAppDbContext<AppDbContext>(configuration["DEFAULT_CONNECTION"]);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/start", async (app) =>
{
    var db = (AppDbContext)app.RequestServices.GetService<BaseAuditableDbContext>()!;
    var study = new Study(Guid.NewGuid());
    db.Studies.Add(study);
    await db.SaveChangesAsync();
    var eventBus = app.RequestServices.GetService<IEventBus>()!;
    eventBus.Publish(new FlgStudyReceivedIntegrationEvent(study.Id));
});

app.Services.MigrateAppDbContext<AppDbContext>();

var eventBus = app.Services.GetService<IEventBus>()!;
eventBus.Subscribe<FlgStudySuccessIntegrationEvent>();
eventBus.Subscribe<FlgStudyFailedIntegrationEvent>();

app.Run();