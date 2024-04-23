using System.Globalization;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Factory;

public class FlightFactory
{
    // public static Flight CreateFromString(string s)
    // {
    //     // force invariant number format to parse correctly numbers with dot as the decimal separator
    //     var nfi = NumberFormatInfo.InvariantInfo;
    //     var split = s.Split(',');
    //     return new Flight(
    //         ulong.Parse(split[1], nfi),
    //         ulong.Parse(split[2], nfi),
    //         ulong.Parse(split[3], nfi),
    //         DateTime.Parse(split[4]),
    //         DateTime.Parse(split[5]),
    //         float.Parse(split[6], nfi),
    //         float.Parse(split[7], nfi),
    //         float.Parse(split[8], nfi),
    //         ulong.Parse(split[9], nfi),
    //         Array.ConvertAll(split[10].TrimStart('[').TrimEnd(']').Split(';'), ulong.Parse),
    //         Array.ConvertAll(split[11].TrimStart('[').TrimEnd(']').Split(';'), ulong.Parse)
    //     );
    // }

    public static Flight CreateFromString2(string s, Airport origin, Airport target, Plane plane, List<Crew> crew,
        List<ILoadable> load)
    {
        // force invariant number format to parse correctly numbers with dot as the decimal separator
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = s.Split(',');
        return new Flight(
            ulong.Parse(split[1], nfi),
            origin,
            target,
            DateTime.Parse(split[4]),
            DateTime.Parse(split[5]),
            float.Parse(split[6], nfi),
            float.Parse(split[7], nfi),
            float.Parse(split[8], nfi),
            plane, crew.ToArray(), load.ToArray()
        );
    }


    public static Flight CreateFromMessage(Message message, FlightSystem flightSystem)
    {
        var messageLength = BitConverter.ToUInt32(message.MessageBytes, 3);
        var crewCount = BitConverter.ToUInt16(message.MessageBytes, 55);
        var loadCount = BitConverter.ToUInt16(message.MessageBytes, 57 + 8 * crewCount);
        var crewIds = new ulong[crewCount];
        Buffer.BlockCopy(message.MessageBytes, 57, crewIds, 0, 8 * crewCount);
        var loadIds = new ulong[loadCount];
        Buffer.BlockCopy(message.MessageBytes, 59 + 8 * crewCount, loadIds, 0, 8 * loadCount);

        var originId = BitConverter.ToUInt64(message.MessageBytes, 15);
        var targetId = BitConverter.ToUInt64(message.MessageBytes, 23);
        var planeId = BitConverter.ToUInt64(message.MessageBytes, 47);

        Airport origin;
        Airport target;
        Plane plane;
        List<Crew> crew = [];
        List<ILoadable> load = [];

        if (!flightSystem.ObjectIds.ContainsKey(originId)) throw new Exception($"originId not found: {originId}");
        origin = (Airport)flightSystem.ObjectIds[originId];
        if (!flightSystem.ObjectIds.ContainsKey(targetId)) throw new Exception($"targetId not found: {targetId}");
        target = (Airport)flightSystem.ObjectIds[targetId];
        if (!flightSystem.ObjectIds.ContainsKey(planeId)) throw new Exception($"planeId not found: {planeId}");
        plane = (Plane)flightSystem.ObjectIds[planeId];

        foreach (var x in crewIds)
        {
            if (!flightSystem.ObjectIds.ContainsKey(x))
                throw new Exception($"crewId not found: {x}");
            crew.Add((Crew)flightSystem.ObjectIds[x]);
        }

        foreach (var x in loadIds)
        {
            if (!flightSystem.ObjectIds.ContainsKey(x))
                throw new Exception($"loadId not found: {x}");
            load.Add((ILoadable)flightSystem.ObjectIds[x]);
        }

        return new Flight(
            BitConverter.ToUInt64(message.MessageBytes, 7),
            origin,
            target,
            UnixTimeStamp.ToDateTime(BitConverter.ToInt64(message.MessageBytes, 31)),
            UnixTimeStamp.ToDateTime(BitConverter.ToInt64(message.MessageBytes, 39)),
            0,
            0,
            null,
            plane,
            crew.ToArray(),
            load.ToArray()
        );
    }
}