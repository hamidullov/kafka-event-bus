using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;
using FlgSeries.Microservice.Domain;

namespace FlgSeries.Microservice.Query.Models;

public class StudySeriesDto : 
    IMapFrom<StudySeries>,  
    IMapTo<StudySeries>, 
    IMapTo<StudySeriesEventModel>, 
    IMapFrom<StudySeriesEventModel>
{
    public int Id { get; set; }
    public string SeriesId { get; set; }
    public ThicknessDto Thickness { get; set; }
    public IEnumerable<StudySeriesInstanceDto> StudySeriesInstances { get; set; }
}