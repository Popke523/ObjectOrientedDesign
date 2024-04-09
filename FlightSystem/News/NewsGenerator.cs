using ObjectOrientedDesign.FlightSystem.News.Media;

namespace ObjectOrientedDesign.FlightSystem.News;

public class NewsGenerator
{
    private readonly IEnumerator<(Medium medium, IReportable reportable)> _pairEnumerator;

    public NewsGenerator(IEnumerable<Medium> media, IEnumerable<IReportable> reportables)
    {
        _pairEnumerator =
            media.SelectMany<Medium, IReportable, (Medium, IReportable)>(_ => reportables,
                (medium, reportable) => (medium, reportable)).GetEnumerator();
    }

    ~NewsGenerator()
    {
        _pairEnumerator.Dispose();
    }

    public string? GenerateNextNews()
    {
        if (!_pairEnumerator.MoveNext()) return null;
        var current = _pairEnumerator.Current;
        return current.reportable.Report(current.medium);
    }
}