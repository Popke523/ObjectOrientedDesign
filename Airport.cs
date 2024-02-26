using System.Globalization;

namespace ObjectOrientedDesign
{
    public class Airport : IImport
    {
        public ulong ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public string Country { get; set; }

        public Airport(ulong id, string name, string code, float longitude, float latitude, float amsl, string country)
        {
            ID = id;
            Name = name;
            Code = code;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = amsl;
            Country = country;
        }
    }

    public class AirportFactory : IFactory
    {
        public IImport CreateFromString(string s)
        {
            NumberFormatInfo nfi = NumberFormatInfo.InvariantInfo;
            string[] split = s.Split(',');
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
}
