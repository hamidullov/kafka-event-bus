using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgStudies;
using MediatR;

namespace FlgStudies.Microservice.DomainEvents;

public class StudyStateChangedDomainEventHandler : INotificationHandler<StudyStateChangedDomainEvent>
{
    private readonly IEventBus _eventBus;

    public StudyStateChangedDomainEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task Handle(StudyStateChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        var study = notification.Study;

        if (study.IsCompletionFailedState())
        {
            _eventBus.Publish(new FlgStudyFailedIntegrationEvent(study.Id, study.GetFailedType(), study.ErrorMessage));
        }

        if (study.IsCompletionSuccessState())
        {
            _eventBus.Publish(new FlgStudySuccessIntegrationEvent(study.Id));
        }
        
        return Task.CompletedTask;
    }
}