using ObjectOrientedDesign.FlightSystem.News.Media;

namespace ObjectOrientedDesign.FlightSystem.News;

public class NewsGenerator
{
    public List<Medium> Media;
    public List<IReportable> Reportables;
    public IEnumerator<string?> StringEnumerator;

    public NewsGenerator(List<Medium> media, List<IReportable> reportables)
    {
        Media = media;
        Reportables = reportables;
        StringEnumerator = NewsIterator.AllNews(this).GetEnumerator();
    }

    public string? GenerateNextNews()
    {
        if (!StringEnumerator.MoveNext()) return null;
        return StringEnumerator.Current;
    }
}