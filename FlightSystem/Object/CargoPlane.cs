using System.Globalization;
using NetworkSourceSimulator;

namespace ObjectOrientedDesign.FlightSystem.Object;

public class CargoPlane : FlightSystemObject
{
    public CargoPlane(ulong id, string serial, string country, string model, float maxLoad)
    {
        ID = id;
        Serial = serial;
        Country = country;
        Model = model;
        MaxLoad = maxLoad;
    }

    public ulong ID { get; set; }
    public string Serial { get; set; }
    public string Country { get; set; }
    public string Model { get; set; }
    public float MaxLoad { get; set; }

    public static CargoPlane CreateFromString(string s)
    {
        // force invariant number format to parse correctly numbers with dot as the decimal separator
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new CargoPlane(
            ulong.Parse(split[1], nfi),
            split[2],
            split[3],
            split[4],
            float.Parse(split[5], nfi)
        );
    }

    public static CargoPlane CreateFromMessage(Message message)
    {
        var messageLength = BitConverter.ToUInt32(message.MessageBytes, 3);
        var modelLength = BitConverter.ToUInt16(message.MessageBytes, 28);

        return new CargoPlane(
            BitConverter.ToUInt64(message.MessageBytes, 7),
            BitConverter.ToString(message.MessageBytes, 15, 10),
            BitConverter.ToString(message.MessageBytes, 25, 3),
            BitConverter.ToString(message.MessageBytes, 30, modelLength),
            BitConverter.ToSingle(message.MessageBytes, 30 + modelLength)
        );
    }
}