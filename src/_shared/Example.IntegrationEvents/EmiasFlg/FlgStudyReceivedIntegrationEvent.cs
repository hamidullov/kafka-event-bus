namespace Example.IntegrationEvents.EmiasFlg;

public class FlgStudyReceivedIntegrationEvent : StudyIntegrationEvent
{
    public FlgStudyReceivedIntegrationEvent(Guid studyId) : base(studyId)
    {
    }
}