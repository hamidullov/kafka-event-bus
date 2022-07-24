using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgStudies;
using FlgValidations.Microservice.Commands.ValidateStudy;
using MediatR;

namespace FlgValidations.Microservice.Consumers;

public class FlgStudyStartedIntegrationEventHandler : IIntegrationEventHandler<FlgStudyStartedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public FlgStudyStartedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(FlgStudyStartedIntegrationEvent @event)
    {
        await _mediator.Send(new ValidateStudyCommand(@event.StudyId));
    }
}