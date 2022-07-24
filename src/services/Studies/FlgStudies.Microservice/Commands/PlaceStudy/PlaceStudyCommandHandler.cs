using AutoMapper;
using Example.Cqrs;
using Example.EfCore;
using Example.EventBus.Abstractions;
using FlgStudies.Microservice.Domain.Services;
using FlgStudies.Microservice.Persistence;
using MediatR;


namespace FlgStudies.Microservice.Commands.PlaceStudy;

public class PlaceStudyCommandHandler : ICommandHandler<PlaceStudyCommand>
{
    private readonly AppDbContext _db;
    private readonly IEventBus _eventBus;
    private readonly IMapper _mapper;

    public PlaceStudyCommandHandler(BaseAuditableDbContext db, IEventBus eventBus, IMapper mapper)
    {
        _eventBus = eventBus;
        _mapper = mapper;
        _db = (AppDbContext)db;
    }

    public async Task<Unit> Handle(PlaceStudyCommand request, CancellationToken cancellationToken)
    {
        var study = StudyGenerator.Generate(request.StudyId);
        _db.Studies.Add(study);
        return Unit.Value;
    }
}