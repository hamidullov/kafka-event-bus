using Example.EventBus.Abstractions;

namespace Example.IntegrationEvents.FlgStudies;

public class FlgStudyStartedIntegrationEvent : StudyIntegrationEvent
{
    public FlgStudyStartedIntegrationEvent(Guid studyId) : base(studyId)
    {
        StudyId = studyId;
    }
}