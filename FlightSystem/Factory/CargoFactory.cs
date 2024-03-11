using System.Globalization;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class CargoFactory : IFactory
{
    public FlightSystemObject CreateFromString(string s)
    {
        // force invariant number format to parse correctly numbers with dot as the decimal separator
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new Cargo(
            ulong.Parse(split[1], nfi),
            float.Parse(split[2], nfi),
            split[3],
            split[4]
        );
    }
}