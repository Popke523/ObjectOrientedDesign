using System.Globalization;

namespace ObjectOrientedDesign
{
    public class PassengerPlane : FlightSystemObject
    {
        public ulong ID { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public ushort FirstClassSize { get; set; }
        public ushort BusinessClassSize { get; set; }
        public ushort EconomyClassSize { get; set; }

        public PassengerPlane(ulong id, string serial, string country, string model, ushort firstClassSize, ushort businessClassSize, ushort economyClassSize)
        {
            ID = id;
            Serial = serial;
            Country = country;
            Model = model;
            FirstClassSize = firstClassSize;
            BusinessClassSize = businessClassSize;
            EconomyClassSize = economyClassSize;
        }
    }

    public class PassengerPlaneFactory : IFactory
    {
        public FlightSystemObject CreateFromString(string s)
        {
            // force invariant number format to parse correctly numbers with dot as the decimal separator
            NumberFormatInfo nfi = NumberFormatInfo.InvariantInfo;
            string[] split = s.Split(',');
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
}
