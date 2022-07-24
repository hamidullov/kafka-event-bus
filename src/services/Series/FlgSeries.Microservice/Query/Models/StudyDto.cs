using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;
using FlgSeries.Microservice.Domain;

namespace FlgSeries.Microservice.Query.Models;

public class StudyDto :
    IMapTo<Study>,
    IMapFrom<Study>,
    IMapTo<StudyEventModel>,
    IMapFrom<StudyEventModel>
{
    public Guid Id { get; set; }
    public string SopStudyId { get; set; }
    public StudyState State { get; set; }
    public string? ErrorMessage { get; set; }
   
}