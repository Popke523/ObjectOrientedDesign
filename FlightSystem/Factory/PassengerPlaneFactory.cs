using System.Globalization;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class PassengerPlaneFactory : IFactory
{
    public FlightSystemObject CreateFromString(string s)
    {
        // force invariant number format to parse correctly numbers with dot as the decimal separator
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new PassengerPlane(
            ulong.Parse(split[1], nfi),
            split[2],
            split[3],
            split[4],
            ushort.Parse(split[5], nfi),
            ushort.Parse(split[6], nfi),
            ushort.Parse(split[7], nfi)
        );
    }
}