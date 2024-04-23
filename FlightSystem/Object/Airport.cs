using ObjectOrientedDesign.FlightSystem.News;
using ObjectOrientedDesign.FlightSystem.News.Media;

namespace ObjectOrientedDesign.FlightSystem.Object;

public class Airport(ulong id, string name, string code, float longitude, float latitude, float amsl, string country)
    : FlightSystemObject(id), IReportable
{
    public string Name { get; set; } = name;
    public string Code { get; set; } = code;
    public float Longitude { get; set; } = longitude;
    public float Latitude { get; set; } = latitude;
    public float Amsl { get; set; } = amsl;
    public string Country { get; set; } = country;


    public string Report(Medium m)
    {
        return m.Report(this);
    }
}