using System.Globalization;
using System.Text;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class CargoFactory
{
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
            Encoding.ASCII.GetString(message.MessageBytes, 19, 6).TrimEnd('\0'),
            Encoding.ASCII.GetString(message.MessageBytes, 27, descriptionLength).TrimEnd('\0')
        );
    }
}