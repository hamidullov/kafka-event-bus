using Example.Common.Exceptions;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgValidations;
using FlgStudies.Microservice.Domain;
using FlgStudies.Microservice.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlgStudies.Microservice.Consumers;

public class FlgStudyValidateSuccessIntegrationEventHandler : IIntegrationEventHandler<FlgStudyValidateSuccessIntegrationEvent>
{
    private readonly AppDbContext _db;
    private IEventBus _eventBus;

    public FlgStudyValidateSuccessIntegrationEventHandler(AppDbContext db, IEventBus eventBus)
    {
        _db = db;
        _eventBus = eventBus;
    }

    public async Task Handle(FlgStudyValidateSuccessIntegrationEvent @event)
    {
        var study = await _db.Studies.FirstOrDefaultAsync(x => x.Id == @event.StudyId);
        if (study == null)
        {
            throw new AppNotFoundException(nameof(Study),  @event.StudyId);
        }

        study.ValidationSuccess();
        await _db.SaveChangesAsync();
        
    }
}