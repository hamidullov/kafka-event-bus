using AutoMapper;
using Example.Cqrs;
using Example.EfCore;
using FlgSeries.Microservice.Domain;
using FlgSeries.Microservice.Persistence;
using MediatR;

namespace FlgSeries.Microservice.Commands.AddStudy;

public class AddStudyCommandHandler : ICommandHandler<AddStudyCommand>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public AddStudyCommandHandler(BaseAuditableDbContext db, IMapper mapper)
    {
        _mapper = mapper;
        _db = (AppDbContext)db;
    }

    public Task<Unit> Handle(AddStudyCommand request, CancellationToken cancellationToken)
    {
        var study = _mapper.Map<Study>(request.Study);
        _db.Studies.Add(study);
        return Task.FromResult(Unit.Value);
    }
}