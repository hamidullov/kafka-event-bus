using Example.Common.Exceptions;
using Example.DDD;

namespace FlgSeries.Microservice.Domain;

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

    public string SopStudyId { get; private set; }
    public StudyState State { get; private set; }
    public string? ErrorMessage { get; private set; }
    public IEnumerable<StudySeries> Series { get; private set; }

    public StudySeries? DefinedSeries { get; private set; }

    public StudySeries DefineSeries()
    {
        if (DefinedSeries != null)
            return DefinedSeries;
        
        var series = Series.FirstOrDefault(x => Equals(x.Thickness, new Thickness(3)));
        DefinedSeries = series ?? throw new DomainException($"Не найдена подходящая серия для исследования {Id}");
        return series;
    }
}