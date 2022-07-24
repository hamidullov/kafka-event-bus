
using Example.DDD;

namespace FlgSeries.Microservice.Domain;

public class StudySeries : AuditableEntity
{
    protected StudySeries()
    {
    }

    public StudySeries(string seriesId, Thickness thickness, IEnumerable<StudySeriesInstance> studySeriesInstances)
    {
        SeriesId = seriesId;
        Thickness = thickness;
        StudySeriesInstances = studySeriesInstances;
    }

    public string SeriesId { get; private set; }
    public Thickness Thickness { get; private set; }
    public IEnumerable<StudySeriesInstance> StudySeriesInstances { get; private set; }
}