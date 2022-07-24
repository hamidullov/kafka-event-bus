using AutoMapper;
using AutoMapper.QueryableExtensions;
using Example.Cqrs;
using FlgSeries.Microservice.Persistence;
using FlgSeries.Microservice.Query.Models;
using Microsoft.EntityFrameworkCore;

namespace FlgSeries.Microservice.Query.GetDefinedSeriesByStudyId;

public class GetStudyWithDefinedSeriesByIdQueryHandler : IQueryHandler<GetStudyWithDefinedSeriesByIdQuery, StudyWithDefinedSeriesDto>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetStudyWithDefinedSeriesByIdQueryHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public Task<StudyWithDefinedSeriesDto> Handle(GetStudyWithDefinedSeriesByIdQuery request, CancellationToken cancellationToken)
    {
        return _db.Studies
            .ProjectTo<StudyWithDefinedSeriesDto>(_mapper.ConfigurationProvider)
            .FirstAsync(x => x.Id == request.StudyId, cancellationToken: cancellationToken);
    }
}