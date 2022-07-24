namespace FlgStudies.Microservice.Domain.Services;

public static class StudyGenerator
{
    private static readonly Random Rnd = new ();

    public static Study Generate(Guid studyId)
    {
        var studySopId = "1.2.40.0.13.1.262578633557574299035350793449" + Rnd.NextInt64(99999).ToString("00000");
        var series = GenerateSeries(studySopId, 3);
        return new Study(studyId, studySopId, series);
    }

    private static List<StudySeries> GenerateSeries(string studySopId, int count)
    {
        var result = new List<StudySeries>();
        for (int i = 0; i < count; i++)
        {
            var seriesId = $"{studySopId}.{i + 1}";
            var instances = GenerateSeriesInstances(seriesId, 10);
            result.Add(new StudySeries(seriesId, new Thickness(3), instances));
        }

        return result;
    }

    private static List<StudySeriesInstance> GenerateSeriesInstances(string seriesId, int count)
    {
        var result = new List<StudySeriesInstance>();
        for (int i = 0; i < count; i++)
        {
            var instanceId = $"{seriesId}.{i + 1}";
            result.Add(new StudySeriesInstance(instanceId));
        }

        return result;
    }   
}