using System.Globalization;

namespace ObjectOrientedDesign
{
    public class Flight : FlightSystemObject
    {
        public ulong ID { get; set; }
        public ulong OriginID { get; set; }
        public ulong TargetID { get; set; }
        public string TakeoffTime { get; set; }
        public string LandingTime { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public ulong PlaneID { get; set; }
        public ulong[] Crew { get; set; }
        public ulong[] Load { get; set; }

        public Flight(ulong id, ulong originID, ulong targetID, string takeoffTime, string landingTime, float longitude, float latitude, float amsl, ulong planeID, ulong[] crew, ulong[] load)
        {
            ID = id;
            OriginID = originID;
            TargetID = targetID;
            TakeoffTime = takeoffTime;
            LandingTime = landingTime;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = amsl;
            PlaneID = planeID;
            Crew = crew;
            Load = load;
        }
    }

    public class FlightFactory : IFactory
    {
        public FlightSystemObject CreateFromString(string s)
        {
            // force invariant number format to parse correctly numbers with dot as the decimal separator
            NumberFormatInfo nfi = NumberFormatInfo.InvariantInfo;
            string[] split = s.Split(',');
            return new Flight(
                ulong.Parse(split[1], nfi),
                ulong.Parse(split[2], nfi),
                ulong.Parse(split[3], nfi),
                split[4],
                split[5],
                float.Parse(split[6], nfi),
                float.Parse(split[7], nfi),
                float.Parse(split[8], nfi),
                ulong.Parse(split[9], nfi),
                Array.ConvertAll<string, ulong>(split[10].TrimStart('[').TrimEnd(']').Split(';'), ulong.Parse),
                Array.ConvertAll<string, ulong>(split[11].TrimStart('[').TrimEnd(']').Split(';'), ulong.Parse)
            );
        }
    }
}
