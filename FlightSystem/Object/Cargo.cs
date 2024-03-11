using System.Globalization;
using NetworkSourceSimulator;

namespace ObjectOrientedDesign.FlightSystem.Object;

public class Cargo : FlightSystemObject
{
    public Cargo(ulong id, float weight, string code, string description)
    {
        ID = id;
        Weight = weight;
        Code = code;
        Description = description;
    }

    public ulong ID { get; set; }
    public float Weight { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }

    public static Cargo CreateFromString(string s)
    {
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new Cargo(
            ulong.Parse(split[1], nfi),
            float.Parse(split[2], nfi),
            split[3],
            split[4]
        );
    }

    public static Cargo CreateFromMessage(Message message)
    {
        var messageLength = BitConverter.ToUInt32(message.MessageBytes, 3);
        var descriptionLength = BitConverter.ToUInt16(message.MessageBytes, 25);

        return new Cargo(
            BitConverter.ToUInt64(message.MessageBytes, 7),
            BitConverter.ToSingle(message.MessageBytes, 15),
            BitConverter.ToString(message.MessageBytes, 19, 6),
            BitConverter.ToString(message.MessageBytes, 27, descriptionLength)
        );
    }
}