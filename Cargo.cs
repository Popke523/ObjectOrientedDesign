using System.Globalization;

namespace ObjectOrientedDesign
{
    public class Cargo : FlightSystemObject
    {
        public ulong ID { get; set; }
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(ulong id, float weight, string code, string description)
        {
            ID = id;
            Weight = weight;
            Code = code;
            Description = description;
        }
    }

    public class CargoFactory : IFactory
    {
        public FlightSystemObject CreateFromString(string s)
        {
            // force invariant number format to parse correctly numbers with dot as the decimal separator
            NumberFormatInfo nfi = NumberFormatInfo.InvariantInfo;
            string[] split = s.Split(',');
            return new Cargo(
                ulong.Parse(split[1], nfi),
                float.Parse(split[2], nfi),
                split[3],
                split[4]
            );
        }
    }
}
