using Example.EventBus.Abstractions;

namespace Example.IntegrationEvents;

public class StudyIntegrationEvent : IntegrationEvent
{
    public StudyIntegrationEvent(Guid studyId) : base(studyId.ToString())
    {
        StudyId = studyId;
    }

    public Guid StudyId { get; set; }
}