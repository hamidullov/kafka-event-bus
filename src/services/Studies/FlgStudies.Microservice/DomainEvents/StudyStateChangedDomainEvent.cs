using FlgStudies.Microservice.Domain;
using MediatR;

namespace FlgStudies.Microservice.DomainEvents;

public class StudyStateChangedDomainEvent : INotification
{
    public StudyStateChangedDomainEvent(Study study)
    {
        Study = study;
    }

    public Study Study { get; set; }
}