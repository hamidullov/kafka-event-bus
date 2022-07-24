using EmiasFlgGateway.Persistence;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgStudies;
using Microsoft.EntityFrameworkCore;

namespace EmiasFlgGateway.Consumers;

public class FlgStudySuccessIntegrationEventHandler : IIntegrationEventHandler<FlgStudySuccessIntegrationEvent>
{
    private readonly AppDbContext _db;

    public FlgStudySuccessIntegrationEventHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task Handle(FlgStudySuccessIntegrationEvent @event)
    {
        var study = await _db.Studies.FirstOrDefaultAsync(x => x.Id ==  @event.StudyId);
        if (study == null)
        {
            return;
        }
        
        study.Complete();
        await _db.SaveChangesAsync();
    }
}