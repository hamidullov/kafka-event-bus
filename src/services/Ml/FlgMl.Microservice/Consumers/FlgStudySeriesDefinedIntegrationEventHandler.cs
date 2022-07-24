using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgMl;
using Example.IntegrationEvents.FlgSeries;

namespace FlgMl.Microservice.Consumers;

public class FlgStudySeriesDefinedIntegrationEventHandler : IIntegrationEventHandler<FlgStudyDefineSeriesSuccessIntegrationEvent>
{
    private readonly ILogger<FlgStudySeriesDefinedIntegrationEventHandler> _logger;
    private readonly IEventBus _eventBus;

    public FlgStudySeriesDefinedIntegrationEventHandler(
        ILogger<FlgStudySeriesDefinedIntegrationEventHandler> logger, 
        IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task Handle(FlgStudyDefineSeriesSuccessIntegrationEvent @event)
    {
        _eventBus.Publish(new FlgStudyMlResearchedIntegrationEvent(@event.StudyId));
    }
}