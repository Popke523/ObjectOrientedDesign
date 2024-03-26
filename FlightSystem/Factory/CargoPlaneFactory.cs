using System.Globalization;
using System.Text;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class CargoPlaneFactory
{
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
            Encoding.ASCII.GetString(message.MessageBytes, 15, 10).TrimEnd('\0'),
            Encoding.ASCII.GetString(message.MessageBytes, 25, 3).TrimEnd('\0'),
            Encoding.ASCII.GetString(message.MessageBytes, 30, modelLength).TrimEnd('\0'),
            BitConverter.ToSingle(message.MessageBytes, 30 + modelLength)
        );
    }
}