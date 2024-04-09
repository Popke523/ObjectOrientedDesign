namespace ObjectOrientedDesign.FlightSystem.News;

public static class NewsIterator
{
    public static IEnumerable<string> AllNews(NewsGenerator ng)
    {
        foreach (var m in ng.Media)
        foreach (var r in ng.Reportables)
            yield return r.Report(m);
    }
}