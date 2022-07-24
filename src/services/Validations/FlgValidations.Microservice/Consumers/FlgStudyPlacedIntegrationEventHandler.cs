using AutoMapper;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgStudies;
using FlgValidations.Microservice.Commands.AddStudy;
using MediatR;

namespace FlgValidations.Microservice.Consumers;

public class FlgStudyPlacedIntegrationEventHandler : IIntegrationEventHandler<FlgStudyPlacedIntegrationEvent>
{
    private readonly ILogger<FlgStudyPlacedIntegrationEventHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper; 

    public FlgStudyPlacedIntegrationEventHandler(ILogger<FlgStudyPlacedIntegrationEventHandler> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Handle(FlgStudyPlacedIntegrationEvent @event)
    {
        var command = _mapper.Map<AddStudyCommand>(@event);
        await _mediator.Send(command);
    }
}