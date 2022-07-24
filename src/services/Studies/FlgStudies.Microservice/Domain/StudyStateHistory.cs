using Example.DDD;

namespace FlgStudies.Microservice.Domain;

public class StudyStateHistory : AuditableEntity
{
    protected StudyStateHistory() {}
    public StudyStateHistory(StudyState state)
    {
        State = state;
    }
    
    public StudyState State { get; private set; }

}