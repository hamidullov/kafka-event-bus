namespace Example.IntegrationEvents.FlgStudies.Models;

public class StudySeriesEventModel
{
    public int Id { get; set; }
    public string SeriesId { get; set; }
    public ThicknessEventModel Thickness { get; set; }
    public IEnumerable<StudySeriesInstanceEventModel> StudySeriesInstances { get; set; }
}