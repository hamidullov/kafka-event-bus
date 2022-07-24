using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;
using FlgStudies.Microservice.Domain;

namespace FlgStudies.Microservice.Queries.Models;

public class StudyDto : IMapFrom<Study>, IMapTo<StudyEventModel>
{
    public Guid Id { get; set; }
    public string SopStudyId { get; set; }
    public IEnumerable<StudySeriesDto> Series { get; set; }
}