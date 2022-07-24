using Example.IntegrationEvents.FlgStudies;
using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;

namespace FlgValidations.Microservice.Commands.Models;

public class StudySeriesInstanceDto : IMapFrom<StudySeriesInstanceEventModel>, IMapTo<Domain.StudySeriesInstance>
{
    public int Id { get; set; }
    public string SopInstanceId { get; set; }
}