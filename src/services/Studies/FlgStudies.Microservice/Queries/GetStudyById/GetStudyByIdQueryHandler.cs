using AutoMapper;
using AutoMapper.QueryableExtensions;
using Example.Cqrs;
using FlgStudies.Microservice.Persistence;
using FlgStudies.Microservice.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace FlgStudies.Microservice.Queries.GetStudyById;

public class GetStudyByIdQueryHandler : IQueryHandler<GetStudyByIdQuery, StudyDto>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetStudyByIdQueryHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public Task<StudyDto> Handle(GetStudyByIdQuery request, CancellationToken cancellationToken)
    {
        return _db.Studies
            .ProjectTo<StudyDto>(_mapper.ConfigurationProvider)
            .FirstAsync(x => x.Id == request.StudyId, cancellationToken: cancellationToken);
    }
}