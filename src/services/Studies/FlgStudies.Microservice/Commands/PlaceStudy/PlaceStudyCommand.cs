using Example.Cqrs;
using MediatR;

namespace FlgStudies.Microservice.Commands.PlaceStudy;

public class PlaceStudyCommand : ICommand
{
    public PlaceStudyCommand(Guid studyId)
    {
        StudyId = studyId;
    }

    public Guid StudyId { get; set; }
   
}