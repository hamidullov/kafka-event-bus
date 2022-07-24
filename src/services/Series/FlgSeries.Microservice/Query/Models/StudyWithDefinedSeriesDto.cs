namespace FlgSeries.Microservice.Query.Models;

public class StudyWithDefinedSeriesDto : StudyDto
{
    public StudySeriesDto? DefinedSeries { get; set; }
}