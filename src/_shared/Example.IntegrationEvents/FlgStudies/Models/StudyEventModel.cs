namespace Example.IntegrationEvents.FlgStudies.Models;

public class StudyEventModel
{
    public Guid Id { get; set; }
    public string SopStudyId { get; set; }
    public IEnumerable<StudySeriesEventModel> Series { get; set; }
}