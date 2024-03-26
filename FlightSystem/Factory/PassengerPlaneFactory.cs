using System.Globalization;
using System.Text;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class PassengerPlaneFactory
{
    public static PassengerPlane CreateFromString(string s)
    {
        // force invariant number format to parse correctly numbers with dot as the decimal separator
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new PassengerPlane(
            ulong.Parse(split[1], nfi),
            split[2],
            split[3],
            split[4],
            ushort.Parse(split[5], nfi),
            ushort.Parse(split[6], nfi),
            ushort.Parse(split[7], nfi)
        );
    }

    public static PassengerPlane CreateFromMessage(Message message)
    {
        var messageLength = BitConverter.ToUInt32(message.MessageBytes, 3);
        var modelLength = BitConverter.ToUInt16(message.MessageBytes, 28);

        return new PassengerPlane(
            BitConverter.ToUInt16(message.MessageBytes, 7),
            Encoding.ASCII.GetString(message.MessageBytes, 15, 10).TrimEnd('\0'),
            Encoding.ASCII.GetString(message.MessageBytes, 25, 3).TrimEnd('\0'),
            Encoding.ASCII.GetString(message.MessageBytes, 30, modelLength).TrimEnd('\0'),
            BitConverter.ToUInt16(message.MessageBytes, 30 + modelLength),
            BitConverter.ToUInt16(message.MessageBytes, 32 + modelLength),
            BitConverter.ToUInt16(message.MessageBytes, 34 + modelLength)
        );
    }
}