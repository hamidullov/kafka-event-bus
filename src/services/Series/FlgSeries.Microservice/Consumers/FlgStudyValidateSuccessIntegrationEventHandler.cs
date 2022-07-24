using AutoMapper;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgSeries;
using Example.IntegrationEvents.FlgStudies.Models;
using Example.IntegrationEvents.FlgValidations;
using FlgSeries.Microservice.Commands.DefineStudySeries;
using FlgSeries.Microservice.Query.GetDefinedSeriesByStudyId;
using MediatR;

namespace FlgSeries.Microservice.Consumers;

public class FlgStudyValidateSuccessIntegrationEventHandler
    : IIntegrationEventHandler<FlgStudyValidateSuccessIntegrationEvent>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILogger<FlgStudyValidateSuccessIntegrationEventHandler> _logger;
    private readonly IEventBus _eventBus;

    public FlgStudyValidateSuccessIntegrationEventHandler(
        IMapper mapper,
        IMediator mediator,
        ILogger<FlgStudyValidateSuccessIntegrationEventHandler> logger,
        IEventBus eventBus)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task Handle(FlgStudyValidateSuccessIntegrationEvent @event)
    {
        try
        {
            await _mediator.Send(new DefineStudySeriesCommand(@event.StudyId));
            var studyWithDefinedSeries = await _mediator.Send(new GetStudyWithDefinedSeriesByIdQuery(@event.StudyId));
            _eventBus.Publish(new FlgStudyDefineSeriesSuccessIntegrationEvent(studyWithDefinedSeries.Id,
                _mapper.Map<StudySeriesEventModel>(studyWithDefinedSeries.DefinedSeries)));
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Ошибка выбора серии для исследования {StudyId}", @event.StudyId);
            _eventBus.Publish(new FlgStudyDefineSeriesFailedIntegrationEvent(@event.StudyId, e.Message));
        }
    }
}