using Example.DDD;

namespace FlgMl.Microservice.Domain;

public class Study : TAuditableEntity<Guid>
{
    protected Study()
    {
    }

    public Study(Guid id, string sopStudyId, IEnumerable<StudySeries> series)
    {
        Id = id;
        SopStudyId = sopStudyId;
        Series = series;
        State = StudyState.Placed;
    }

    public Guid Id { get; private set; }
    public string SopStudyId { get; private set; }
    public StudyState State { get; private set; }
    public string? ErrorMessage { get; private set; }
    public IEnumerable<StudySeries> Series { get; private set; }

    public bool Validate()
    {
        var rnd = new Random();
        State = rnd.Next(100) < 85 ? StudyState.Validated : StudyState.ValidationFailed;
        if (State != StudyState.Validated)
        {
            ErrorMessage = "Ошибка валидации исследования";
        }

        return State == StudyState.Validated;
    }
}