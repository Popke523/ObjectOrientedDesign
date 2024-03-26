using System.Globalization;
using System.Text;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class AirportFactory
{
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
            Encoding.ASCII.GetString(message.MessageBytes, 17, nameLength).TrimEnd('\0'),
            Encoding.ASCII.GetString(message.MessageBytes, 17, 3).TrimEnd('\0'),
            BitConverter.ToSingle(message.MessageBytes, 20 + nameLength),
            BitConverter.ToSingle(message.MessageBytes, 24 + nameLength),
            BitConverter.ToSingle(message.MessageBytes, 28 + nameLength),
            Encoding.ASCII.GetString(message.MessageBytes, 32 + nameLength, 3).TrimEnd('\0')
        );
    }
}