namespace ObjectOrientedDesign.FlightSystem.Object;

public class Crew(ulong id, string name, ulong age, string phone, string email, ushort practice, string role)
    : FlightSystemObject
{
    public ulong Id { get; set; } = id;
    public string Name { get; set; } = name;
    public ulong Age { get; set; } = age;
    public string Phone { get; set; } = phone;
    public string Email { get; set; } = email;
    public ushort Practice { get; set; } = practice;
    public string Role { get; set; } = role;
}