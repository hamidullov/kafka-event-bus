using Example.Cqrs;
using MediatR;

namespace FlgValidations.Microservice.Commands.ValidateStudy;

public class ValidateStudyCommand : ICommand<Promise<Unit>>
{
    public ValidateStudyCommand(Guid studyId)
    {
        StudyId = studyId;
    }

    public Guid StudyId { get; set; }
}