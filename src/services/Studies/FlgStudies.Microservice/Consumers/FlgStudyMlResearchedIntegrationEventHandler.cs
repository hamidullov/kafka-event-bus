using Example.Common.Exceptions;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgMl;
using FlgStudies.Microservice.Domain;
using FlgStudies.Microservice.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlgStudies.Microservice.Consumers;

public class FlgStudyMlResearchedIntegrationEventHandler : IIntegrationEventHandler<FlgStudyMlResearchedIntegrationEvent>
{
    private readonly AppDbContext _db;

    public FlgStudyMlResearchedIntegrationEventHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task Handle(FlgStudyMlResearchedIntegrationEvent @event)
    {
        var study = await _db.Studies
            .FirstOrDefaultAsync(x => x.Id == @event.StudyId);
        if (study == null)
        {
            throw new AppNotFoundException(nameof(Study),  @event.StudyId);
        }

        study.MlResearched();
        await _db.SaveChangesAsync();
    }
}