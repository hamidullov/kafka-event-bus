using Example.Common.Exceptions;
using Example.Cqrs;
using Example.EfCore;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgStudies;
using FlgStudies.Microservice.Domain;
using FlgStudies.Microservice.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlgStudies.Microservice.Commands.StartStudy;

public class StartStudyCommandHandler : ICommandHandler<StartStudyCommand, Promise<Unit>>
{
    private readonly AppDbContext _db;
    private readonly IEventBus _eventBus;

    public StartStudyCommandHandler(BaseAuditableDbContext db, IEventBus eventBus)
    {
        _db = (AppDbContext)db;
        _eventBus = eventBus;
    }

    public async Task<Promise<Unit>> Handle(StartStudyCommand request, CancellationToken cancellationToken)
    {
        var study = await _db.Studies.FirstOrDefaultAsync(x => x.Id == request.StudyId, cancellationToken);
        if (study == null)
        {
            throw new AppNotFoundException(nameof(Study), request.StudyId);
        }
        study.Start();

        return new Promise<Unit>(Unit.Value, () =>
        {
            _eventBus.Publish(new FlgStudyStartedIntegrationEvent(study.Id));
        });
    }
}