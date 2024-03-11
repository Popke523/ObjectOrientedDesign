using System.Globalization;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class PassengerFactory : IFactory
{
    // force invariant number format to parse correctly numbers with dot as the decimal separator
    public FlightSystemObject CreateFromString(string s)
    {
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new Passenger(
            ulong.Parse(split[1], nfi),
            split[2],
            ulong.Parse(split[3], nfi),
            split[4],
            split[5],
            split[6],
            ulong.Parse(split[7], nfi)
        );
    }
}