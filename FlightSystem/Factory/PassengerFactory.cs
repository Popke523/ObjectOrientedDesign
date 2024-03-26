using System.Globalization;
using System.Text;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class PassengerFactory
{
    // force invariant number format to parse correctly numbers with dot as the decimal separator

    public static Passenger CreateFromString(string s)
    {
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new Passenger(
            ulong.Parse(split[1], nfi),
            split[2],
            ulong.Parse(split[3], nfi),
            split[4],
            split[5],
            split[6],
            ulong.Parse(split[7], nfi)
        );
    }

    public static Passenger CreateFromMessage(Message message)
    {
        var messageLength = BitConverter.ToUInt32(message.MessageBytes, 3);
        var nameLength = BitConverter.ToUInt16(message.MessageBytes, 15);
        var emailLength = BitConverter.ToUInt16(message.MessageBytes, 31 + nameLength);

        return new Passenger(
            BitConverter.ToUInt64(message.MessageBytes, 7),
            Encoding.ASCII.GetString(message.MessageBytes, 17, nameLength).TrimEnd('\0'),
            BitConverter.ToUInt16(message.MessageBytes, 17 + nameLength),
            Encoding.ASCII.GetString(message.MessageBytes, 19 + nameLength, 12).TrimEnd('\0'),
            Encoding.ASCII.GetString(message.MessageBytes, 33 + nameLength, emailLength).TrimEnd('\0'),
            Encoding.ASCII.GetString(message.MessageBytes, 33 + nameLength + emailLength, 1).TrimEnd('\0'),
            BitConverter.ToUInt64(message.MessageBytes, 34 + nameLength + emailLength)
        );
    }
}