using Example.EventBus.Abstractions;

namespace Example.IntegrationEvents.FlgMl;

public class FlgStudyMlResearchedIntegrationEvent : StudyIntegrationEvent
{
    public FlgStudyMlResearchedIntegrationEvent(Guid studyId) : base(studyId)
    {
    }
}