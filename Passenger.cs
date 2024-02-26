using System.Globalization;

namespace ObjectOrientedDesign
{
    public class Passenger : IImport
    {
        public ulong ID { get; set; }
        public string Name { get; set; }
        public ulong Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public ulong Miles { get; set; }

        public Passenger(ulong id, string name, ulong age, string phone, string email, string @class, ulong miles)
        {
            ID = id;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Class = @class;
            Miles = miles;
        }
    }

    public class PassengerFactory : IFactory
    {
        public IImport CreateFromString(string s)
        {
            NumberFormatInfo nfi = NumberFormatInfo.InvariantInfo;
            string[] split = s.Split(',');
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
    }
}
