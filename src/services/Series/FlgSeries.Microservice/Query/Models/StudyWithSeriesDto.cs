using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;

namespace FlgSeries.Microservice.Query.Models;

public class StudyWithSeriesDto : StudyDto
{
    public List<StudySeriesDto> Series { get; set; }
}