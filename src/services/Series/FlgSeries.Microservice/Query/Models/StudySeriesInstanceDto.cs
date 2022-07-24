using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;
using FlgSeries.Microservice.Domain;

namespace FlgSeries.Microservice.Query.Models;

public class StudySeriesInstanceDto :
    IMapTo<StudySeriesInstance>,
    IMapFrom<StudySeriesInstance>, 
    IMapTo<StudySeriesInstanceEventModel>, 
    IMapFrom<StudySeriesInstanceEventModel>
{
    public int Id { get; set; }
    public string SopInstanceId { get; set; }
}