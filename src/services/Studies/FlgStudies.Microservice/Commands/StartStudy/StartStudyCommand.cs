using Example.Cqrs;
using MediatR;

namespace FlgStudies.Microservice.Commands.StartStudy;

public class StartStudyCommand : ICommand<Promise<Unit>>
{
    public StartStudyCommand(Guid studyId)
    {
        StudyId = studyId;
    }

    public Guid StudyId { get; set; }
}