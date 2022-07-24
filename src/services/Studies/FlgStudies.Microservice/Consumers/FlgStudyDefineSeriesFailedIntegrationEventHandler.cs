using Example.Common.Exceptions;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgSeries;
using FlgStudies.Microservice.Domain;
using FlgStudies.Microservice.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlgStudies.Microservice.Consumers;

public class FlgStudyDefineSeriesFailedIntegrationEventHandler : IIntegrationEventHandler<FlgStudyDefineSeriesFailedIntegrationEvent>
{
    private readonly AppDbContext _db;

    public FlgStudyDefineSeriesFailedIntegrationEventHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task Handle(FlgStudyDefineSeriesFailedIntegrationEvent @event)
    {
        var study = await _db.Studies.FirstOrDefaultAsync(x => x.Id == @event.StudyId);
        if (study == null)
        {
            throw new AppNotFoundException(nameof(Study),  @event.StudyId);
        }

        study.DefineSeriesFailed(@event.ErrorMessage);
        await _db.SaveChangesAsync();
        
    }
}