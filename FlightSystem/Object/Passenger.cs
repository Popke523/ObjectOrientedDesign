namespace ObjectOrientedDesign.FlightSystem.Object;

public class Passenger(ulong id, string name, ulong age, string phone, string email, string @class, ulong miles)
    : FlightSystemObject
{
    public ulong Id { get; set; } = id;
    public string Name { get; set; } = name;
    public ulong Age { get; set; } = age;
    public string Phone { get; set; } = phone;
    public string Email { get; set; } = email;
    public string Class { get; set; } = @class;
    public ulong Miles { get; set; } = miles;
}