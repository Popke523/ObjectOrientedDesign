namespace ObjectOrientedDesign.FlightSystem.Object;

public class Passenger : FlightSystemObject
{
    public Passenger(ulong id, string name, ulong age, string phone, string email, string @class, ulong miles)
    {
        Id = id;
        Name = name;
        Age = age;
        Phone = phone;
        Email = email;
        Class = @class;
        Miles = miles;
    }

    public ulong Id { get; set; }
    public string Name { get; set; }
    public ulong Age { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Class { get; set; }
    public ulong Miles { get; set; }
}