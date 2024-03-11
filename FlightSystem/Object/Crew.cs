using System.Globalization;
using NetworkSourceSimulator;

namespace ObjectOrientedDesign.FlightSystem.Object;

public class Crew : FlightSystemObject
{
    public Crew(ulong id, string name, ulong age, string phone, string email, ushort practice, string role)
    {
        Id = id;
        Name = name;
        Age = age;
        Phone = phone;
        Email = email;
        Practice = practice;
        Role = role;
    }

    public ulong Id { get; set; }
    public string Name { get; set; }
    public ulong Age { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public ushort Practice { get; set; }
    public string Role { get; set; }


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
            BitConverter.ToString(message.MessageBytes, 15, nameLength),
            BitConverter.ToUInt16(message.MessageBytes, 17 + nameLength),
            BitConverter.ToString(message.MessageBytes, 19 + nameLength, 12),
            BitConverter.ToString(message.MessageBytes, 31 + nameLength, emailLength),
            BitConverter.ToUInt16(message.MessageBytes, 33 + nameLength + emailLength),
            BitConverter.ToString(message.MessageBytes, 35 + nameLength + emailLength, 1)
        );
    }
}