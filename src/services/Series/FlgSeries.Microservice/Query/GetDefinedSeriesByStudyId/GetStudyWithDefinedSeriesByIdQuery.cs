using Example.Cqrs;
using FlgSeries.Microservice.Query.Models;

namespace FlgSeries.Microservice.Query.GetDefinedSeriesByStudyId;

public class GetStudyWithDefinedSeriesByIdQuery : IQuery<StudyWithDefinedSeriesDto>
{
    public GetStudyWithDefinedSeriesByIdQuery(Guid studyId)
    {
        StudyId = studyId;
    }

    public Guid StudyId { get; set; }
}