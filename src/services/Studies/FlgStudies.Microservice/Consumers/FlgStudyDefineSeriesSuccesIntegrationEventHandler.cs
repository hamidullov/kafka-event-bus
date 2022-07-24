using Example.Common.Exceptions;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgSeries;
using FlgStudies.Microservice.Domain;
using FlgStudies.Microservice.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlgStudies.Microservice.Consumers;

public class FlgStudyDefineSeriesSuccessIntegrationEventHandler : IIntegrationEventHandler<FlgStudyDefineSeriesSuccessIntegrationEvent>
{
    private readonly AppDbContext _db;

    public FlgStudyDefineSeriesSuccessIntegrationEventHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task Handle(FlgStudyDefineSeriesSuccessIntegrationEvent @event)
    {
        var study = await _db.Studies.FirstOrDefaultAsync(x => x.Id == @event.StudyId);
        if (study == null)
        {
            throw new AppNotFoundException(nameof(Study),  @event.StudyId);
        }

        study.DefineSeriesSuccess();
        await _db.SaveChangesAsync();
    }
}