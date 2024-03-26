using System.Globalization;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class FlightFactory
{
    public static Flight CreateFromString(string s)
    {
        // force invariant number format to parse correctly numbers with dot as the decimal separator
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new Flight(
            ulong.Parse(split[1], nfi),
            ulong.Parse(split[2], nfi),
            ulong.Parse(split[3], nfi),
            DateTime.Parse(split[4]),
            DateTime.Parse(split[5]),
            float.Parse(split[6], nfi),
            float.Parse(split[7], nfi),
            float.Parse(split[8], nfi),
            ulong.Parse(split[9], nfi),
            Array.ConvertAll(split[10].TrimStart('[').TrimEnd(']').Split(';'), ulong.Parse),
            Array.ConvertAll(split[11].TrimStart('[').TrimEnd(']').Split(';'), ulong.Parse)
        );
    }


    public static Flight CreateFromMessage(Message message)
    {
        var messageLength = BitConverter.ToUInt32(message.MessageBytes, 3);
        var crewCount = BitConverter.ToUInt16(message.MessageBytes, 55);
        var loadCount = BitConverter.ToUInt16(message.MessageBytes, 57 + 8 * crewCount);
        var crew = new ulong[crewCount];
        Buffer.BlockCopy(message.MessageBytes, 57, crew, 0, 8 * crewCount);
        var load = new ulong[loadCount];
        Buffer.BlockCopy(message.MessageBytes, 59 + 8 * crewCount, load, 0, 8 * loadCount);

        return new Flight(
            BitConverter.ToUInt64(message.MessageBytes, 7),
            BitConverter.ToUInt64(message.MessageBytes, 15),
            BitConverter.ToUInt64(message.MessageBytes, 23),
            UnixTimeStamp.ToDateTime(BitConverter.ToInt64(message.MessageBytes, 31)),
            UnixTimeStamp.ToDateTime(BitConverter.ToInt64(message.MessageBytes, 39)),
            0,
            0,
            null,
            BitConverter.ToUInt64(message.MessageBytes, 47),
            crew,
            load
        );
    }
}