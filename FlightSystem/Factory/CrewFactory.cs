using System.Globalization;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class CrewFactory : IFactory
{
    public FlightSystemObject CreateFromString(string s)
    {
        // force invariant number format to parse correctly numbers with dot as the decimal separator
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new Crew(
            ulong.Parse(split[1], nfi),
            split[2],
            ulong.Parse(split[3], nfi),
            split[4],
            split[5],
            ushort.Parse(split[6], nfi),
            split[7]
        );
    }
}