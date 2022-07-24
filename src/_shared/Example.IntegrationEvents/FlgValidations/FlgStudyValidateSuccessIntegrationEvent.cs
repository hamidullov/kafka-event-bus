using Example.EventBus.Abstractions;

namespace Example.IntegrationEvents.FlgValidations;

public class FlgStudyValidateSuccessIntegrationEvent : StudyIntegrationEvent
{
    public FlgStudyValidateSuccessIntegrationEvent(Guid studyId) : base(studyId)
    {
        StudyId = studyId;
    }
}