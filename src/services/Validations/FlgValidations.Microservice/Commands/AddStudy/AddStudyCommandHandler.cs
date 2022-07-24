using AutoMapper;
using Example.Cqrs;
using Example.EfCore;
using FlgValidations.Microservice.Domain;
using FlgValidations.Microservice.Persistence;
using MediatR;

namespace FlgValidations.Microservice.Commands.AddStudy;

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