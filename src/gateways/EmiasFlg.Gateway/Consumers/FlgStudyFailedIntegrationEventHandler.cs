using EmiasFlgGateway.Persistence;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgStudies;
using Microsoft.EntityFrameworkCore;

namespace EmiasFlgGateway.Consumers;

public class FlgStudyFailedIntegrationEventHandler : IIntegrationEventHandler<FlgStudyFailedIntegrationEvent>
{
    private readonly AppDbContext _db;

    public FlgStudyFailedIntegrationEventHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task Handle(FlgStudyFailedIntegrationEvent @event)
    {
        var study = await _db.Studies.FirstOrDefaultAsync(x => x.Id ==  @event.StudyId);
        if (study == null)
        {
            return;
        }
        
        study.Complete(@event.ErrorMessage);
        await _db.SaveChangesAsync();
    }
}