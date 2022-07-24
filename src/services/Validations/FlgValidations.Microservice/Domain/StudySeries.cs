
using Example.DDD;

namespace FlgValidations.Microservice.Domain;

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

    public string SeriesId { get; set; }
    public Thickness Thickness { get; set; }
    public IEnumerable<StudySeriesInstance> StudySeriesInstances { get; set; }
}