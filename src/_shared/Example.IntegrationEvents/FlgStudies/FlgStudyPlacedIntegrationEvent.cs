using Example.IntegrationEvents.FlgStudies.Models;

namespace Example.IntegrationEvents.FlgStudies;

public class FlgStudyPlacedIntegrationEvent : StudyIntegrationEvent
{
    public FlgStudyPlacedIntegrationEvent(StudyEventModel study) : base(study.Id)
    {
        Study = study;
    }

    public StudyEventModel Study { get; set; }
}