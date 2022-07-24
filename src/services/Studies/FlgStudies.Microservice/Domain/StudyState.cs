namespace FlgStudies.Microservice.Domain;

public enum StudyState
{
    Placed = 1,
    Started = 2,
    ValidationSuccess = 3,
    ValidationFailed = 4,
    SeriesDefined = 5,
    SeriesDefineFailed = 6,
    MlResearchStarted = 7,
    MlResearchFinished = 8,
    MlResearchFailed = 9,
    ScGenerated = 10,
    SrGenerated = 11,
    Finished = 12
}