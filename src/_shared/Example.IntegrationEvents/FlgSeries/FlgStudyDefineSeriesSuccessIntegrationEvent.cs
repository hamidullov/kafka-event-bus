using Example.IntegrationEvents.FlgStudies.Models;

namespace Example.IntegrationEvents.FlgSeries;

public class FlgStudyDefineSeriesSuccessIntegrationEvent : StudyIntegrationEvent
{
    public FlgStudyDefineSeriesSuccessIntegrationEvent(Guid studyId,  StudySeriesEventModel definedSeries) : base(studyId)
    {
        DefinedSeries = definedSeries;
    }

    public StudySeriesEventModel DefinedSeries { get; set; }
}