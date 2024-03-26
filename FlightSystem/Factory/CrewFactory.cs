using System.Globalization;
using System.Text;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class CrewFactory
{
    public static Crew CreateFromString(string s)
    {
        // force invariant number format to parse correctly numbers with dot as the decimal separator
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new Crew(
            ulong.Parse(split[1], nfi),
            split[2],
            ulong.Parse(split[3], nfi),
            split[4],
            split[5],
            ushort.Parse(split[6], nfi),
            split[7]
        );
    }

    public static Crew CreateFromMessage(Message message)
    {
        var messageLength = BitConverter.ToUInt32(message.MessageBytes, 3);
        var nameLength = BitConverter.ToUInt16(message.MessageBytes, 15);
        var emailLength = BitConverter.ToUInt16(message.MessageBytes, 31 + nameLength);

        return new Crew(
            BitConverter.ToUInt64(message.MessageBytes, 7),
            Encoding.ASCII.GetString(message.MessageBytes, 17, nameLength).TrimEnd('\0'),
            BitConverter.ToUInt16(message.MessageBytes, 17 + nameLength),
            Encoding.ASCII.GetString(message.MessageBytes, 19 + nameLength, 12).TrimEnd('\0'),
            Encoding.ASCII.GetString(message.MessageBytes, 33 + nameLength, emailLength).TrimEnd('\0'),
            BitConverter.ToUInt16(message.MessageBytes, 33 + nameLength + emailLength),
            Encoding.ASCII.GetString(message.MessageBytes, 35 + nameLength + emailLength, 1).TrimEnd('\0')
        );
    }
}