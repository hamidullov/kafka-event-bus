using Example.Common.Enums;
namespace Example.IntegrationEvents.FlgStudies;

public class FlgStudyFailedIntegrationEvent : StudyIntegrationEvent
{
    public FlgStudyFailedIntegrationEvent(Guid studyId, StudyFailedType type, string errorMessage) : base(studyId)
    {
        Type = type;
        ErrorMessage = errorMessage;
    }
    public StudyFailedType Type { get; set; }
    public string ErrorMessage { get; set; }
}