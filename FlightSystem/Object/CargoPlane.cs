using ObjectOrientedDesign.FlightSystem.News;
using ObjectOrientedDesign.FlightSystem.News.Media;

namespace ObjectOrientedDesign.FlightSystem.Object;

public class CargoPlane(ulong id, string serial, string country, string model, float maxLoad)
    : Plane(id, serial, country, model), IReportable
{
    public float MaxLoad { get; set; } = maxLoad;

    public string Report(Medium m)
    {
        return m.Report(this);
    }
}