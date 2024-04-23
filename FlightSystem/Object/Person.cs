namespace ObjectOrientedDesign.FlightSystem.Object;

public class Person(ulong id, string name, ulong age, string phone, string email) : FlightSystemObject(id)
{
    public string Name { get; set; } = name;
    public ulong Age { get; set; } = age;
    public string Phone { get; set; } = phone;
    public string Email { get; set; } = email;
}