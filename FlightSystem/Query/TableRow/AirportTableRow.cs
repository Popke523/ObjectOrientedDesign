using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Query;

public class AirportTableRow : TableRow
{
    private readonly Airport _airport;
    public AirportTableRow(Airport airport)
    {
        this._airport = airport;
        Parsers = new Dictionary<string, Func<string, IComparable>>
        {
            { "id", s => ulong.Parse(s) },
            { "name", s => s },
            { "code", s => s },
            { "long", s => float.Parse(s) },
            { "lat", s => float.Parse(s) },
            { "amsl", s => float.Parse(s) },
            { "country", s => s }
        };
    }

    public override IComparable Get(string name)
    {
        var fields = new Dictionary<string, IComparable>
        {
            { "id", _airport.Id },
            { "name", _airport.Name },
            { "code", _airport.Code },
            { "long", _airport.Longitude },
            { "longitude", _airport.Longitude },
            { "lat", _airport.Latitude },
            { "latitude", _airport.Latitude },
            { "amsl", _airport.Amsl },
            { "country", _airport.Country }
        };

        return fields[name.ToLower()];
    }
}