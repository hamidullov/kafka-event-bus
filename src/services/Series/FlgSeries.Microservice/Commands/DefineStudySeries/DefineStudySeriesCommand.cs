using Example.Cqrs;
using MediatR;

namespace FlgSeries.Microservice.Commands.DefineStudySeries;

public class DefineStudySeriesCommand : ICommand
{
    public DefineStudySeriesCommand(Guid studyId)
    {
        StudyId = studyId;
    }

    public Guid StudyId { get; set; }
}