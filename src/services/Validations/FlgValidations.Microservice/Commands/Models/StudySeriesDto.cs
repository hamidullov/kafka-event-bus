using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;
using FlgValidations.Microservice.Domain;

namespace FlgValidations.Microservice.Commands.Models;

public class StudySeriesDto : IMapFrom<StudySeriesEventModel>, IMapTo<StudySeries>
{
    public int Id { get; set; }
    public string SeriesId { get; set; }
    public ThicknessDto Thickness { get; set; }
    public IEnumerable<StudySeriesInstanceDto> StudySeriesInstances { get; set; }
}