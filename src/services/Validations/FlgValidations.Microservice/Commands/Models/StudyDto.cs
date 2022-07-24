using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;
using FlgValidations.Microservice.Domain;

namespace FlgValidations.Microservice.Commands.Models;

public class StudyDto : IMapTo<Study>, IMapFrom<StudyEventModel>
{
    public Guid Id { get; set; }
    public string SopStudyId { get; set; }
    public IEnumerable<StudySeriesDto> Series { get; set; }
}