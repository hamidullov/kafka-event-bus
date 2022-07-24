using Example.Cqrs;
using FlgStudies.Microservice.Queries.Models;

namespace FlgStudies.Microservice.Queries.GetStudyById;

public class GetStudyByIdQuery : IQuery<StudyDto>
{
    public GetStudyByIdQuery(Guid studyId)
    {
        StudyId = studyId;
    }

    public Guid StudyId { get; set; }
}