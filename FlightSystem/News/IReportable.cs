using ObjectOrientedDesign.FlightSystem.News.Media;

namespace ObjectOrientedDesign.FlightSystem.News;

public interface IReportable
{
    public string Report(Medium m);
}