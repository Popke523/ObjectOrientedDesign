using System.Globalization;

namespace ObjectOrientedDesign
{
    public class CargoPlane : IImport
    {
        public ulong ID { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public float MaxLoad { get; set; }

        public CargoPlane(ulong id, string serial, string country, string model, float maxLoad)
        {
            ID = id;
            Serial = serial;
            Country = country;
            Model = model;
            MaxLoad = maxLoad;
        }
    }

    public class CargoPlaneFactory : IFactory
    {
        public IImport CreateFromString(string s)
        {
            NumberFormatInfo nfi = NumberFormatInfo.InvariantInfo;
            string[] split = s.Split(',');
            return new CargoPlane(
                ulong.Parse(split[1], nfi),
                split[2],
                split[3],
                split[4],
                float.Parse(split[5], nfi)
            );
        }
    }
}
