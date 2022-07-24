using Example.EventBus.Abstractions;

namespace Example.IntegrationEvents.FlgValidations;

public class FlgStudyValidateFailedIntegrationEvent : StudyIntegrationEvent
{
    public FlgStudyValidateFailedIntegrationEvent(Guid studyId, string errorMessage) : base(studyId)
    {
        ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; set; }
}