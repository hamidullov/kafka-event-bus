namespace Example.IntegrationEvents.FlgSeries;

public class FlgStudyDefineSeriesFailedIntegrationEvent : StudyIntegrationEvent
{
    public FlgStudyDefineSeriesFailedIntegrationEvent(Guid studyId, string errorMessage) : base(studyId)
    {
        ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; set; }
}