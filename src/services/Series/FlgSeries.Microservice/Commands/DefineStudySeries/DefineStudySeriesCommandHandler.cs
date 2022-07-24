using AutoMapper;
using Example.Common.Exceptions;
using Example.Cqrs;
using Example.EfCore;
using Example.EventBus.Abstractions;
using Example.IntegrationEvents.FlgSeries;
using Example.IntegrationEvents.FlgValidations;
using FlgSeries.Microservice.Domain;
using FlgSeries.Microservice.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlgSeries.Microservice.Commands.DefineStudySeries;

public class DefineStudySeriesCommandHandler : ICommandHandler<DefineStudySeriesCommand>
{
    private readonly AppDbContext _db;
    private readonly IEventBus _eventBus;
    private readonly ILogger<DefineStudySeriesCommandHandler> _logger;
    private readonly IMapper _mapper;

    public DefineStudySeriesCommandHandler(BaseAuditableDbContext db, IEventBus eventBus, ILogger<DefineStudySeriesCommandHandler> logger, IMapper mapper)
    {
        _eventBus = eventBus;
        _logger = logger;
        _mapper = mapper;
        _db = (AppDbContext)db;
    }

    public async Task<Unit> Handle(DefineStudySeriesCommand request, CancellationToken cancellationToken)
    {
        var study = await _db.Studies
            .Include(x => x.Series)
            .FirstOrDefaultAsync(x => x.Id == request.StudyId, cancellationToken);
        if (study == null)
        {
            throw new AppNotFoundException(nameof(Study), request.StudyId);
        }
        study.DefineSeries();
        
        return Unit.Value;
    }
}