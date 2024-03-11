using System.Globalization;
using NetworkSourceSimulator;

namespace ObjectOrientedDesign.FlightSystem.Object;

public class PassengerPlane : FlightSystemObject
{
    public PassengerPlane(ulong id, string serial, string country, string model, ushort firstClassSize,
        ushort businessClassSize, ushort economyClassSize)
    {
        Id = id;
        Serial = serial;
        Country = country;
        Model = model;
        FirstClassSize = firstClassSize;
        BusinessClassSize = businessClassSize;
        EconomyClassSize = economyClassSize;
    }

    public ulong Id { get; set; }
    public string Serial { get; set; }
    public string Country { get; set; }
    public string Model { get; set; }
    public ushort FirstClassSize { get; set; }
    public ushort BusinessClassSize { get; set; }
    public ushort EconomyClassSize { get; set; }

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
            BitConverter.ToString(message.MessageBytes, 15, 10),
            BitConverter.ToString(message.MessageBytes, 25, 3),
            BitConverter.ToString(message.MessageBytes, 30, modelLength),
            BitConverter.ToUInt16(message.MessageBytes, 30 + modelLength),
            BitConverter.ToUInt16(message.MessageBytes, 32 + modelLength),
            BitConverter.ToUInt16(message.MessageBytes, 34 + modelLength)
        );
    }
}