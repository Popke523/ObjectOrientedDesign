namespace ObjectOrientedDesign.FlightSystem.Object;

public class Crew(ulong id, string name, ulong age, string phone, string email, ushort practice, string role)
    : Person(id, name, age, phone, email)
{
    public ushort Practice { get; set; } = practice;
    public string Role { get; set; } = role;
}