using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;
using FlgStudies.Microservice.Domain;

namespace FlgStudies.Microservice.Queries.Models;

public class StudySeriesDto : IMapFrom<StudySeries>, IMapTo<StudySeriesEventModel>
{
    public int Id { get; set; }
    public string SeriesId { get; set; }
    public ThicknessDto Thickness { get; set; }
    public IEnumerable<StudySeriesInstanceDto> StudySeriesInstances { get; set; }
}