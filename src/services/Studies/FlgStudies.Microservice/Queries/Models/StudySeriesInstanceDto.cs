using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;
using FlgStudies.Microservice.Domain;

namespace FlgStudies.Microservice.Queries.Models;

public class StudySeriesInstanceDto : 
    IMapTo<StudySeriesInstanceEventModel>, 
    IMapFrom<StudySeriesInstance>,
    IMapFrom<StudySeriesInstanceEventModel>
{
    public int Id { get; set; }
    public string SopInstanceId { get; set; }
}