using Example.Common.Exceptions;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgValidations;
using FlgStudies.Microservice.Domain;
using FlgStudies.Microservice.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlgStudies.Microservice.Consumers;

public class FlgStudyValidateFailedIntegrationEventHandler : IIntegrationEventHandler<FlgStudyValidateFailedIntegrationEvent>
{
    private readonly AppDbContext _db;

    public FlgStudyValidateFailedIntegrationEventHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task Handle(FlgStudyValidateFailedIntegrationEvent @event)
    {
        var study = await _db.Studies.FirstOrDefaultAsync(x => x.Id == @event.StudyId);
        if (study == null)
        {
            throw new AppNotFoundException(nameof(Study),  @event.StudyId);
        }

        study.ValidationFailed(@event.ErrorMessage);
        await _db.SaveChangesAsync();
        
    }
}