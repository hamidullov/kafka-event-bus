using Example.Common.Enums;
using Example.EventBus.Abstractions;

namespace Example.IntegrationEvents.FlgStudies;

public class FlgStudySuccessIntegrationEvent : StudyIntegrationEvent
{
    public FlgStudySuccessIntegrationEvent(Guid studyId) : base(studyId)
    {
        StudyId = studyId;
    }
}