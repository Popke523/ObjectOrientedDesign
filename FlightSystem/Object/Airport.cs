using System.Globalization;
using NetworkSourceSimulator;

namespace ObjectOrientedDesign.FlightSystem.Object;

public class Airport : FlightSystemObject
{
    public Airport(ulong id, string name, string code, float longitude, float latitude, float amsl, string country)
    {
        Id = id;
        Name = name;
        Code = code;
        Longitude = longitude;
        Latitude = latitude;
        AMSL = amsl;
        Country = country;
    }

    public ulong Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float AMSL { get; set; }
    public string Country { get; set; }


    public static Airport CreateFromString(string s)
    {
        // force invariant number format to parse correctly numbers with dot as the decimal separator
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
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

    public static Airport CreateFromMessage(Message message)
    {
        var messageLength = BitConverter.ToUInt32(message.MessageBytes, 3);
        var nameLength = BitConverter.ToUInt16(message.MessageBytes, 15);

        return new Airport(
            BitConverter.ToUInt64(message.MessageBytes, 7),
            BitConverter.ToString(message.MessageBytes, 17, nameLength).TrimEnd((char)0),
            BitConverter.ToString(message.MessageBytes, 17, 3),
            BitConverter.ToSingle(message.MessageBytes, 20 + nameLength),
            BitConverter.ToSingle(message.MessageBytes, 24 + nameLength),
            BitConverter.ToSingle(message.MessageBytes, 28 + nameLength),
            BitConverter.ToString(message.MessageBytes, 32 + nameLength, 3)
        );
    }
}