using System.Globalization;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class FlightFactory : IFactory
{
    public FlightSystemObject CreateFromString(string s)
    {
        // force invariant number format to parse correctly numbers with dot as the decimal separator
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new Flight(
            ulong.Parse(split[1], nfi),
            ulong.Parse(split[2], nfi),
            ulong.Parse(split[3], nfi),
            DateTime.Parse(split[4]),
            DateTime.Parse(split[5]),
            float.Parse(split[6], nfi),
            float.Parse(split[7], nfi),
            float.Parse(split[8], nfi),
            ulong.Parse(split[9], nfi),
            Array.ConvertAll(split[10].TrimStart('[').TrimEnd(']').Split(';'), ulong.Parse),
            Array.ConvertAll(split[11].TrimStart('[').TrimEnd(']').Split(';'), ulong.Parse)
        );
    }
}