using System.Globalization;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class AirportFactory : IFactory
{
    public FlightSystemObject CreateFromString(string s)
    {
        // force invariant number format to parse correctly numbers with dot as the decimal separator
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new Airport(
            ulong.Parse(split[1], nfi),
            split[2],
            split[3],
            float.Parse(split[4], nfi),
            float.Parse(split[5], nfi),
            float.Parse(split[6], nfi),
            split[7]
        );
    }
}