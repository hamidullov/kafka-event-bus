using Example.DDD;

namespace FlgSeries.Microservice.Domain;

public class StudySeriesInstance : AuditableEntity
{
    protected StudySeriesInstance()
    {
    }

    public StudySeriesInstance(string sopInstanceId)
    {
        SopInstanceId = sopInstanceId;
    }

    public string SopInstanceId { get; set; }
}