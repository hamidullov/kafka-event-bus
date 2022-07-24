using Example.Common.Enums;
using Example.Common.Exceptions;
using Example.DDD;
using FlgStudies.Microservice.DomainEvents;

namespace FlgStudies.Microservice.Domain;

public class Study : TAuditableEntity<Guid>
{
    private static readonly StudyState[] CompletionFailedStates =
        { StudyState.ValidationFailed, StudyState.MlResearchFailed, StudyState.SeriesDefineFailed };

    protected Study()
    {
    }

    public Study(Guid id, string sopStudyId, IEnumerable<StudySeries> series)
    {
        Id = id;
        SopStudyId = sopStudyId;
        Series = series;
        StateHistory = new List<StudyStateHistory>();
        SetState(StudyState.Placed);
    }

    public string SopStudyId { get; private set; }
    public IEnumerable<StudySeries> Series { get; private set; }

    public StudyState State { get; private set; }
    public List<StudyStateHistory> StateHistory { get; private set; }

    public string? ErrorMessage { get; private set; }

    /// <summary>
    /// Старт исследования
    /// </summary>
    public void Start()
    {
        if (State != StudyState.Placed)
        {
            throw new InvalidStateDomainException(State, StudyState.Placed);
        }

        SetState(StudyState.Started);
    }

    public void ValidationFailed(string errorMessage)
    {
        ErrorMessage = errorMessage;
        SetState(StudyState.ValidationFailed);
    }

    public void ValidationSuccess()
    {
        SetState(StudyState.ValidationSuccess);
    }


    private void SetState(StudyState state)
    {
        State = state;
        AddDomainEvent(new StudyStateChangedDomainEvent(this));
        StateHistory ??= new List<StudyStateHistory>();
        StateHistory.Add(new StudyStateHistory(state));
    }

    public bool IsCompletionFailedState()
    {
        return CompletionFailedStates.Contains(State);
    }

    public StudyFailedType GetFailedType()
    {
        if (!IsCompletionFailedState())
            throw new DomainException("Исследование не в ошибочном состоянии");

        return State switch
        {
            StudyState.ValidationFailed => StudyFailedType.ValidationFailed,
            StudyState.MlResearchFailed => StudyFailedType.MlResearchFailed,
            _ => throw new DomainException("Исследование не в ошибочном состоянии")
        };
    }

    public void MlResearched()
    {
        SetState(StudyState.Finished);
    }

    public bool IsCompletionSuccessState()
    {
        return State == StudyState.Finished;
    }

    public void DefineSeriesFailed(string errorMessage)
    {
        ErrorMessage = errorMessage;
        SetState(StudyState.SeriesDefineFailed);
    }

    public void DefineSeriesSuccess()
    {
        SetState(StudyState.SeriesDefined);
    }
}