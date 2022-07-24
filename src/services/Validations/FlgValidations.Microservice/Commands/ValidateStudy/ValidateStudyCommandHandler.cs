using Example.Common.Exceptions;
using Example.Cqrs;
using Example.EfCore;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgStudies;
using Example.IntegrationEvents.FlgValidations;
using FlgValidations.Microservice.Domain;
using FlgValidations.Microservice.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlgValidations.Microservice.Commands.ValidateStudy;

public class ValidateStudyCommandHandler : ICommandHandler<ValidateStudyCommand, Promise<Unit>>
{
    private readonly AppDbContext _db;
    private readonly IEventBus _eventBus;

    public ValidateStudyCommandHandler(BaseAuditableDbContext db, IEventBus eventBus)
    {
        _eventBus = eventBus;
        _db = (AppDbContext)db;
    }

    public async Task<Promise<Unit>> Handle(ValidateStudyCommand request, CancellationToken cancellationToken)
    {
        var study = await _db.Studies.FirstOrDefaultAsync(x => x.Id == request.StudyId, cancellationToken);
        int i = 0;
        while (study == null && i++ < 5)
        {
            study = await _db.Studies.FirstOrDefaultAsync(x => x.Id == request.StudyId, cancellationToken);
            await Task.Delay(1000 * i, cancellationToken);
        }

        if(study == null)
        {
            throw new AppNotFoundException(nameof(Study), request.StudyId);
        }

        var validated = study.Validate();

        return new Promise<Unit>(Unit.Value, () =>
        {
            if (validated)
            {
                _eventBus.Publish(new FlgStudyValidateSuccessIntegrationEvent(study.Id));
            }
            else
            {
                _eventBus.Publish(new FlgStudyValidateFailedIntegrationEvent(study.Id, study.ErrorMessage!));
            }
        });
    }
}