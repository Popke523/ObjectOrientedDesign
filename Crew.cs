using System.Globalization;

namespace ObjectOrientedDesign
{
    public class Crew : FlightSystemObject
    {
        public ulong ID { get; set; }
        public string Name { get; set; }
        public ulong Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ushort Practice { get; set; }
        public string Role { get; set; }

        public Crew(ulong id, string name, ulong age, string phone, string email, ushort practice, string role)
        {
            ID = id;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Practice = practice;
            Role = role;
        }
    }

    public class CrewFactory : IFactory
    {
        public FlightSystemObject CreateFromString(string s)
        {
            // force invariant number format to parse correctly numbers with dot as the decimal separator
            NumberFormatInfo nfi = NumberFormatInfo.InvariantInfo;
            string[] split = s.Split(',');
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
    }
}
