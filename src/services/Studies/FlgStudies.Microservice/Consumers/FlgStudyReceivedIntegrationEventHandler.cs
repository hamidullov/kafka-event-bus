using AutoMapper;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.EmiasFlg;
using Example.IntegrationEvents.FlgStudies;
using FlgStudies.Microservice.Commands.PlaceStudy;
using FlgStudies.Microservice.Commands.StartStudy;
using FlgStudies.Microservice.Queries.GetStudyById;
using MediatR;

namespace FlgStudies.Microservice.Consumers;

public class FlgStudyReceivedIntegrationEventHandler : IIntegrationEventHandler<FlgStudyReceivedIntegrationEvent>
{
    private readonly ILogger<FlgStudyReceivedIntegrationEventHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public FlgStudyReceivedIntegrationEventHandler(ILogger<FlgStudyReceivedIntegrationEventHandler> logger, IMediator mediator, IMapper mapper, IEventBus eventBus)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    public async Task Handle(FlgStudyReceivedIntegrationEvent @event)
    {
        await _mediator.Send(new PlaceStudyCommand(@event.StudyId));
        var study = await _mediator.Send(new GetStudyByIdQuery(@event.StudyId));
        _eventBus.Publish(new FlgStudyPlacedIntegrationEvent(_mapper.Map<Example.IntegrationEvents.FlgStudies.Models.StudyEventModel>(study)));
        await _mediator.Send(new StartStudyCommand(@event.StudyId));
    }
}